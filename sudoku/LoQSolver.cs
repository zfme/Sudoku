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
            var queueList = new List<ConcurrentQueue<BoardDFSQ>>(65);
            for (int i = 0; i < 65; i++)
            {
                queueList.Add(new ConcurrentQueue<BoardDFSQ>());
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
                        queueList[q].TryPeek(out boardDfsq);
                        if (boardDfsq == null)
                        {
                            continue;
                        }
                        if (boardDfsq.Board.IsSolved())
                        {
                            cozuldu = true;
                            break;
                        }
                        
                        lock (islemLock)
                        {
                            if (boardDfsq.State.Equals(State.Empty))
                            {
                                boardDfsq.State = State.Processing;
                                List<Board> possibleBoards = boardDfsq.Board.FirstEmptyCellPossibleBoards();
                                foreach (var possibleBoard in possibleBoards)
                                {
                                    queueList[q + 1].Enqueue(new BoardDFSQ {Board = possibleBoard, State = State.Empty});
                                }
                                boardDfsq.State = State.Finished;
                            }
                            else if (boardDfsq.State.Equals(State.Finished))
                            {
                                BoardDFSQ boardDfsqDeleted;
                                queueList[q].TryDequeue(out boardDfsqDeleted);
                            }
                            else if (boardDfsq.State.Equals(State.Processing))
                            {
                                break;
                            }
                        }
                        Console.WriteLine("Queue check peek-{0}:{1} thread={2}, i={3}", q, boardDfsq,
                            Thread.CurrentThread.ManagedThreadId, i);
                    }
                }
            });
            return null;
        }
    }
}
