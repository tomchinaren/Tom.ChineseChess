using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine.Util
{
    public class Flow<T>
    {
        private T _curState;
        private Dictionary<T, List<T>> _dict;

        public T CurState { get { return _curState; } }
        public Flow(T initState)
        {
            _curState = initState;
            _dict = new Dictionary<T, List<T>>();
        }

        public void AddState(T fromState,T toState)
        {
            var hasKey = _dict.ContainsKey(fromState);

            List<T> list = null;
            if (!hasKey)
            {
                list = new List<T>();
                _dict.Add(fromState, list);
            }
            list = _dict[fromState];
            
            if (!list.Exists(t => t.Equals(toState)))
            {
                list.Add(toState);
            }
        }

        protected bool CanNext(T newState)
        {
            return true;
        }

        public void Next(T newState)
        {
            if (!CanNext(newState))
            {
                throw new Exception("Can't Next to ");
            }


        }
    }
}
