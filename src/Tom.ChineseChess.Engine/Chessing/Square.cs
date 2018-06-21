using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tom.ChineseChess.Engine
{
    public class Square
    {
        private string _name;
        private List<Chess> _chessList;
        private int _state;
        public int State
        {
            get { return _state; }
        }

        public Square(string name) 
        {
            _name = name;
        }

        public void Sit(Table table)
        {

        }
        public void Ready()
        {

        }

        public void Move()
        {

        }
    }
}
