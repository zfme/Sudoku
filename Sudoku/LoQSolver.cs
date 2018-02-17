using Sudoku.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// Kuyruk listesi ile sudoku çözen sınıftır.
    /// </summary>
    public class LoQSolver
    {
        /// <summary>
        /// Kullanılan thread sayısı
        /// </summary>
        private const int ThreadSayisi = 2;
        /// <summary>
        /// Bir sonraki kuyruğa aktarılabilecek maximum tahta kopyası sayısı
        /// </summary>
        private const int MaxChildCount = 4;
        /// <summary>
        /// Çözüm değerleri
        /// </summary>
        private bool cozuldu = false;
        private Board cozumBoard = null;
        /// <summary>
        /// Kuyruk listesi, thread-safe olması için ConcurrentQueue kullanılmıştır.
        /// </summary>
        private List<ConcurrentQueue<BoardForDFSQueue>> queueList;

        /// <summary>
        /// Verilen tahtayı kuyruk listesi ile çözer ve sonuç olarak tüm hücrelerinin değeri dolu ve geçerli bir tahat nesnesi döndürür.
        /// </summary>
        /// <param name="board">Sudoku problemi tahtası</param>
        /// <returns>Çözüm tahtası</returns>
        public Board Solve(Board board)
        {
            queueList = new List<ConcurrentQueue<BoardForDFSQueue>>(65);
            var boardlocks = new object[65];
            for (int i = 0; i < 65; i++)
            {
                queueList.Add(new ConcurrentQueue<BoardForDFSQueue>());
                boardlocks[i] = new object();
            }
            // ilk queue ya board u ekle
            queueList[0].Enqueue(new BoardForDFSQueue { Board = board, State = BoardProcessState.Empty });
            var options = new ParallelOptions { MaxDegreeOfParallelism = ThreadSayisi };
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < ThreadSayisi; i++)
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
            foreach (Thread t in threads)
            {
                t.Join();
            }
            Console.WriteLine("bitti");
            return cozumBoard;
        }

        /// <summary>
        /// Her bir çözüm thread inin çalıştırdığı fonksiyon
        /// </summary>
        private void Coz()
        {
            Console.WriteLine("Started thread={0}", Thread.CurrentThread.ManagedThreadId);
            while (!cozuldu)
            {
                for (int q = 64; q >= 0; q--)
                {
                    if (cozuldu)
                    {
                        break;
                    }

                    //bool locked = false;
                    //Monitor.TryEnter(boardlocks[q], ref locked);
                    //if (!locked)
                    //{
                    //    //i = 65;
                    //    continue;
                    //}
                    //if (queueList[q].Count == 0)
                    //{
                    //    //Monitor.Exit(boardlocks[q]);
                    //    continue;
                    //}

                    // bakılcak eleman olmalı
                    //try
                    //{
                    if (!queueList[q].TryDequeue(out BoardForDFSQueue boardDfsq))
                    {
                        continue;
                    }
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
                        cozuldu = true;
                        cozumBoard = boardDfsq.Board;
                        break;
                    }
                    int childCount = 0;
                    for (var index = 0; index < 9; index++)
                    {
                        for (var indexy = 0; indexy < 9 && childCount < MaxChildCount; indexy++)
                        {
                            if (boardDfsq.Board.Table[index, indexy].Value == 0)
                            {
                                var usedPossibleValueCount = 0;
                                for (int k = 0; k < boardDfsq.Board.Table[index, indexy].PossibleValues.Count; k++)
                                {
                                    byte possibleValue = boardDfsq.Board.Table[index, indexy].PossibleValues[k];
                                    // boardDfsq.Board.Table[index, indexy].PossibleValues.RemoveAt(k);
                                    //  k--;
                                    BoardForDFSQueue child = boardDfsq.Copy();
                                    child.Board.Table[index, indexy].Value = possibleValue;
                                    if (child.Board.IsValid())
                                    {
                                        child.Board.FillPossibleValues();
                                        child.State = BoardProcessState.Empty;
                                        child.QueueIndex = q + 1;
                                        queueList[q + 1].Enqueue(child);
                                        childCount++;
                                        usedPossibleValueCount++;
                                        if (childCount == MaxChildCount)
                                        {
                                            break;
                                        }
                                    }
                                }
                                boardDfsq.Board.Table[index, indexy].PossibleValues.RemoveRange(0, usedPossibleValueCount);

                            }
                        }
                    }

                    //queueList[q].Dequeue();
                    //Monitor.Exit(boardlocks[q]);


                    //     Console.WriteLine("Queue check peek-{0}:{1} thread={2}, i={3}", q, boardDfsq,
                    //       Thread.CurrentThread.ManagedThreadId, i);
                }
            }
            Console.WriteLine("Ended thread={0}", Thread.CurrentThread.ManagedThreadId);
        }

    }
}
