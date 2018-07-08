using Serilog;
using Sudoku.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// Kuyruk listesi ile sudoku çözen sınıftır.
    /// </summary>
    public class LoQSolver : ISolver
    {
        /// <summary>
        /// Kullanılan thread sayısı
        /// </summary>
        private int _threadSayisi = 2;
        /// <summary>
        /// Bir sonraki kuyruğa aktarılabilecek maximum tahta kopyası sayısı
        /// </summary>
        private int _maxChildCount = 4;
        /// <summary>
        /// Çözüm değerleri
        /// </summary>
        private bool cozuldu = false;
        private Board cozumBoard = null;
        /// <summary>
        /// Kuyruk listesi, thread-safe olması için ConcurrentQueue kullanılmıştır.
        /// </summary>
        private List<Queue<BoardForDFSQueue>> queueList;
        /// <summary>
        /// Queue locks
        /// </summary>
        private object[] boardlocks = new object[65];

        public LoQSolver(int ThreadSayisi, int MaxChildCount)
        {
            _maxChildCount = MaxChildCount;
            _threadSayisi = ThreadSayisi;
        }

        /// <summary>
        /// Verilen tahtayı kuyruk listesi ile çözer ve sonuç olarak tüm hücrelerinin değeri dolu ve geçerli bir tahat nesnesi döndürür.
        /// </summary>
        /// <param name="board">Sudoku problemi tahtası</param>
        /// <returns>Çözüm tahtası</returns>
        public Board Solve(Board board)
        {
            Log.Information("LoQSolver başlıyor: Thread: {0}, MaxChildCount: {1}", _threadSayisi, _maxChildCount);
            queueList = new List<Queue<BoardForDFSQueue>>(65);
            for (int i = 0; i < 65; i++)
            {
                queueList.Add(new Queue<BoardForDFSQueue>());
                boardlocks[i] = new object();
            }
            // ilk queue ya board u ekle
            queueList[0].Enqueue(new BoardForDFSQueue { Board = board, State = BoardProcessState.Empty });
            //var options = new ParallelOptions { MaxDegreeOfParallelism = _threadSayisi };
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < _threadSayisi; i++)
            {
                threads.Add(new Thread(delegate ()
                {
                    Coz();
                }));
            }
            foreach (Thread t in threads)
            {
                t.Start();
            }
            /*
            Parallel.For(0, ThreadSayisi, options, (i, loopState) =>
            {
                Coz();
            });
            */
            Log.Information("LoQSolver threadler bekleniyor");
            foreach (Thread t in threads)
            {
                t.Join();
            }
            Log.Information("LoQSolver bitti");
            return cozumBoard;
        }

        public void Temizle()
        {
            cozuldu = false;
            cozumBoard = null;
        }

        /// <summary>
        /// Her bir çözüm thread inin çalıştırdığı fonksiyon
        /// </summary>
        private void Coz()
        {

            Log.Verbose("Started thread={0}", Thread.CurrentThread.ManagedThreadId);
            while (!cozuldu)
            {
                for (int q = 64; q >= 0; q--)
                {
                    Log.Verbose("Thread={0} trying queue: {1}", Thread.CurrentThread.ManagedThreadId, q);
                    if (cozuldu)
                    {
                        Log.Information("Thread={0} cozuldu en basta: {1}", Thread.CurrentThread.ManagedThreadId, q);
                        break;
                    }

                    bool lockTaken = false;
                    Monitor.TryEnter(boardlocks[q], ref lockTaken);
                    if (!lockTaken)
                    {
                        break;
                    }
                    if (queueList[q].Count == 0)
                    {
                        Log.Information("Thread={0} queuedan eleman peek edemedi: {1}", Thread.CurrentThread.ManagedThreadId, q);
                        Monitor.Exit(boardlocks[q]);
                        continue;
                    }

                    // bakılcak eleman olmalı
                    //try
                    //{
                    BoardForDFSQueue boardDfsq = queueList[q].Peek();
                    //if (!queueList[q].Peek(out ))
                    //{
                        
                    //    Monitor.Exit(boardlocks[q]);
                    //    continue;
                    //}
                    //Monitor.Exit(boardlocks[q]);
                    //}
                    //catch (InvalidOperationException ex)
                    //{
                    //    continue;
                    //}
                    //if (boardDfsq == null)
                    //{
                    //    continue;
                    //}
                    //if (boardDfsq.State.Equals(BoardProcessState.Empty))
                    //{
                    //    boardDfsq.State = BoardProcessState.Processing;
                    //}
                    //else if (boardDfsq.State == BoardProcessState.Processing)
                    //{
                    //    Console.WriteLine("Wrong state");
                    //    break;
                    //}
                    if (boardDfsq.Board.IsSolved())
                    {
                        Log.Information("Thread={0} cozuldu ortada: {1}, {2}", Thread.CurrentThread.ManagedThreadId, q, boardDfsq.Id);
                        cozuldu = true;
                        cozumBoard = boardDfsq.Board;
                        Monitor.Exit(boardlocks[q]);
                        break;
                    }
                    int childCount = 0;
                    bool exit = false;
                    bool clear = true;
                    if (boardDfsq.KilitlendiMi())
                    {
                        queueList[q].Dequeue();
                        Monitor.Exit(boardlocks[q]);
                        continue;
                    }
                    for (var index = 0; index < 9 && childCount < _maxChildCount; index++)
                    {
                        for (var indexy = 0; indexy < 9 && childCount < _maxChildCount; indexy++)
                        {
                            if (boardDfsq.Board.Table[index, indexy].Value == 0)
                            {

                                Log.Verbose("Thread={0} found empty value: [{1},{2}], possible count:{3}", Thread.CurrentThread.ManagedThreadId,
                                    index, indexy, boardDfsq.Board.Table[index, indexy].PossibleValues.Count);
                                var usedPossibleValueCount = 0;
                                for (int k = 0; k < boardDfsq.Board.Table[index, indexy].PossibleValues.Count; k++)
                                {
                                    clear = false;
                                    byte possibleValue = boardDfsq.Board.Table[index, indexy].PossibleValues[k];
                                    // boardDfsq.Board.Table[index, indexy].PossibleValues.RemoveAt(k);
                                    //  k--;
                                    BoardForDFSQueue child = boardDfsq.Copy();
                                    child.Board.Table[index, indexy].Value = possibleValue;
                                   // if (child.Board.IsValid())
                                    //{
                                        if (childCount == _maxChildCount)
                                        {
                                            exit = true;
                                            break;
                                        }
                                        
                                        Log.Verbose("Thread={0} found empty value: [{1},{2}], valid, possible value: {3} {4}", Thread.CurrentThread.ManagedThreadId,
                                            index, indexy, true, possibleValue);
                                        child.Board.FillPossibleValues();
                                        child.State = BoardProcessState.Empty;
                                        child.QueueIndex = q + 1;
                                        queueList[q + 1].Enqueue(child);
                                        childCount++;
                                        usedPossibleValueCount++;

                                  //  }
                                }
                                boardDfsq.Board.Table[index, indexy].PossibleValues.RemoveRange(0, usedPossibleValueCount);
                            }
                            if (exit)
                            {
                                break;
                            }
                        }
                        if (exit)
                        {
                            break;
                        }
                    }
                    if (clear)
                    {
                        queueList[q].Dequeue();
                    }

                    //queueList[q].Dequeue();
                    Monitor.Exit(boardlocks[q]);


                    //     Console.WriteLine("Queue check peek-{0}:{1} thread={2}, i={3}", q, boardDfsq,
                    //       Thread.CurrentThread.ManagedThreadId, i);
                }
            }
            Log.Verbose("Ended thread={0}", Thread.CurrentThread.ManagedThreadId);
        }

    }
}
