using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinder.Visualizer.Data
{
    public class BoardState
    {
        public int StartNode { get; private set; }
        public int EndNode { get; private set; }

        public int[] path { get; private set; } = { };
        public bool mouseDown { get; private set; }
        public IList<int> BlockedNodes { get; private set; } = new List<int>();
        public IList<int> VisitedNodes { get; private set; } = new List<int>();
        public int CurrentNode { get; private set; }

        public event Action OnChange;

        public void SelectStart(int id)
        {
            StartNode = id;
            NotifyStateChanged();
        }
        public void SelectEnd(int id)
        {
            EndNode = id;
            NotifyStateChanged();
        }

        public void setPath(int[] path)
        {
            this.path = path;
            NotifyStateChanged();
        }

        public void clear()
        {
            StartNode = -1;
            EndNode = -1;
            path = new int[] { };
            mouseDown = false;
            CurrentNode = -1;
            BlockedNodes.Clear();
            VisitedNodes.Clear();
            NotifyStateChanged();
        }

        public void visitNode(uint u)
        {
            if (CurrentNode != null)
            {
                VisitedNodes.Add(CurrentNode);
            }
            CurrentNode = Convert.ToInt32(u);
            NotifyStateChanged();
        }

        public void blockNode(int id)
        {
            BlockedNodes.Add(id);
        }

        public void unblockNode(int id)
        {
            BlockedNodes.Remove(id);
        }

        public void toggleClicked()
        {
            mouseDown = !mouseDown;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
