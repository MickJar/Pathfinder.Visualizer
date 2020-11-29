using System.Collections.Generic;
using System.Threading.Tasks;
using ShortestPathResult = Pathfinder.Visualizer.Solvers.ShortestPathResult;
using System;
using Pathfinder.Visualizer.Solvers;
using Dijkstra.NET.Graph;

namespace Pathfinder.Visualizer.Data
{
    public class PathfinderService
    {
        State[,] board;
        private DijkstraSolver dijkstraSolver;

        public Task<State[,]> GetBoard(int size)
        {
            board = new State[size, size];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = State.Unvisited;
                }
            }
            return Task.FromResult(board);
        }

        public async Task<IEnumerable<uint>> FindPath(State[,] board, BoardState boardState)
        {
            var graph = new Graph<int, string>();

            int counter = 0;
            foreach (var node in board)
            {
                graph.AddNode(counter);
                counter++;
            }
            counter = 0;
            foreach (var node in board)
            {
                var rowLength = board.GetLength(1);
                var leftNeighbor = counter - 1;
                var rightNeighbor = counter + 1;
                var downNeighbor = counter + board.GetLength(1);
                var upNeighbor = counter - board.GetLength(1);
                var diagNeighbor = counter + board.GetLength(1) + 1;
                var diagNeighborBehind = counter + board.GetLength(1) - 1;

                if (!boardState.BlockedNodes.Contains(leftNeighbor) && (leftNeighbor % rowLength != 0))
                {
                    graph.Connect((uint)counter, (uint)(leftNeighbor), 1, node.ToString()); // Connect Down
                }
                if (!boardState.BlockedNodes.Contains(rightNeighbor) && (counter % rowLength != 0))
                {
                    graph.Connect((uint)counter, (uint)(rightNeighbor), 1, node.ToString()); // Connect right
                }

                if(!boardState.BlockedNodes.Contains(downNeighbor))
                {
                    graph.Connect((uint)counter, (uint)(downNeighbor), 1, node.ToString()); // Connect Down
                }

                if (!boardState.BlockedNodes.Contains(upNeighbor) && upNeighbor > 0)
                {
                    graph.Connect((uint)counter, (uint)(upNeighbor), 1, node.ToString()); // Connect Down
                }

                if (!boardState.BlockedNodes.Contains(diagNeighbor) && ((counter % board.GetLength(1)) != 0))
                {
                    graph.Connect((uint)counter, (uint)(diagNeighbor), 1, node.ToString()); // Connect diag right
                }

                if (!boardState.BlockedNodes.Contains(diagNeighborBehind) && counter != 0 && ((diagNeighborBehind % (board.GetLength(1))) != 0))
                {
                    graph.Connect((uint)counter, (uint)(diagNeighborBehind), 1, node.ToString()); // Connect diag left
                }


                counter++;
            }


            dijkstraSolver = new DijkstraSolver(boardState);
            ShortestPathResult result = await dijkstraSolver.GetShortestPathAsync(graph, (uint)boardState.StartNode, (uint)boardState.EndNode, Int32.MaxValue);
            
            var path = result.GetPath();
            return path;
        }
    }
}
