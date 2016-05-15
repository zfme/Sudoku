using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace sudoku
{
    class LoQSolver
    {
        public Board Solve(Board board)
        {
            var queueList = new List<Queue<BoardDFSQ>>(65);
            var boardlocks = new object[65];
            for (int i = 0; i < 65; i++)
            {
                queueList.Add(new Queue<BoardDFSQ>());
                boardlocks[i] = new object();
            }
            // ilk queue ya board u ekle
            queueList[0].Enqueue(new BoardDFSQ { Board = board, State = State.Empty });
            // çözüm objesi
            bool cozuldu = false;
            object islemLock = new object();
            var options = new ParallelOptions { MaxDegreeOfParallelism = 8 };
            Parallel.For(0, 8, options, (i, loopState) =>
            {

                Console.WriteLine("Started thread={0}, i={1}", Thread.CurrentThread.ManagedThreadId, i); 
                while (!cozuldu)
                {
                    for (int q = 64; q >= 0; q--)
                    {
                        if (cozuldu)
                        {
                            break;
                        }
                        BoardDFSQ boardDfsq;
                        if (queueList[q].Count==0)
                        {
                            continue;
                        }
                        boardDfsq = queueList[q].Peek();

                        if (boardDfsq == null)
                        {
                            continue;
                        }

                 
                        lock (boardlocks[q])
                        {
                            //if (boardDfsq.Board.IsSolved())
                            //{
                            //    cozuldu = true;
                            //    break;
                            //}
                            if (boardDfsq.State.Equals(State.Empty))
                            {
                                boardDfsq.State = State.Processing;
                            }
                            else if (boardDfsq.State == State.Processing) {
                                break;
                            }
                            else{
                                if (boardDfsq.Board.IsSolved()) 
                                {
                                    cozuldu = true;
                                    break;
                                }
                            }
                        }

                    
                        for (var index = 0; index < 9; index++)
                        {
                            for (var indexy = 0; indexy < 9; indexy++)
                            {
                                if (boardDfsq.Board.Table[index, indexy].Value == 0)
                                {
                                    foreach (var possibleValue in boardDfsq.Board.Table[index, indexy].PossibleValues)
                                    {
                                        BoardDFSQ child = boardDfsq.Copy();
                                        child.Board.Table[index, indexy].Value = possibleValue;
                                       // child.Board.Table[index, indexy].PossibleValues.Remove(possibleValue);
                                        child.Board.FillPossibleValues();
                                        if (child.Board.IsValidDfsq())
                                        {
                                            if (child.Board.IsSolved())
                                            {
                                                cozuldu = true;
                                                return;
                                            }
                                            else {
                                                child.State = State.Empty;
                                                child.QueueNdx = q + 1;
                                                queueList[q + 1].Enqueue(child);
                                            }
                                            
                                        }     
                                    }
                                  
                                }
                            }
                        }

                        queueList[q].Dequeue();   
                     
                        
                        Console.WriteLine("Queue check peek-{0}:{1} thread={2}, i={3}", q, boardDfsq,
                            Thread.CurrentThread.ManagedThreadId, i);
                    }
                }
            });

            Console.WriteLine("bitti");
            return null;
        }
    }
}
