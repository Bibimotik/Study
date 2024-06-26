using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5Lib {
    public class Decorator : IWriter {
        protected IWriter? _writer;

        public Decorator(IWriter? writer) {
            this._writer = writer;
        }

        public virtual string? Save(string? message) {
            return _writer?.Save(message);
        }
    }
}