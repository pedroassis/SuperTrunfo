using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class Message<T> {

        public String    name;
        public T         message;
        public String    className;

        public Message(){}

        public Message(String name, T message, String className){
            this.name = name;
            this.message = message;
            this.className = className;
        }

    }
}
