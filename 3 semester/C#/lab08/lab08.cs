using System;

namespace Lab07
{
    public class User
    {
        public int Position { get; set; }
        public int Length { get; set; }

        public User(int position, int length)
        {
            Position = position;
            Length = length;
        }

        public delegate void Fine(User obj, int position);
        public delegate void Increase(User obj, int length);
        public event Fine FineEvent;
        public event Increase IncreaseEvent;

        public void Move(int offset)
        {
            Position += offset;
            FineEvent?.Invoke(this, Position);
        }

        public void Compress(double compressionRatio)
        {
            Length = (int)(Length * compressionRatio);
            IncreaseEvent?.Invoke(this, Length);
        }
    }

    public class Str
    {
        public static string RemoveS(string str)
        {
            char[] sign = { '.', ',', '!', '?', '-', ':' };
            for (var i = 0; i < str.Length; i++)
            {
                if (sign.Contains(str[i]))
                {
                    str = str.Remove(i, 1);
                }
            }
            return str;
        }

        public static string RemoveSpase(string str)
        {
            return str.Replace(" ", string.Empty);
        }

        public static string Upper(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                str = str.Replace(str[i], char.ToUpper(str[i]));
            }

            return str;
        }

        public static string Letter(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                str = str.Replace(str[i], char.ToLower(str[i]));
            }

            return str;
        }

        public static string AddToString(string str)
        {
            return str += "!!!!!!!!!!!!!!";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            User user1 = new User(0, 10);
            User user2 = new User(5, 20);
            User user3 = new User(10, 15);

            user1.FineEvent += (obj, position) => Console.WriteLine($"User1: Fine event выполнено. Новая позиция: {position}");
            user2.FineEvent += (obj, position) => Console.WriteLine($"User2: Fine event выполнено. Новая позиция: {position}");

            user2.IncreaseEvent += (obj, length) => Console.WriteLine($"User2: Increase event выполнено. Новая длина: {length}");
            user3.IncreaseEvent += (obj, length) => Console.WriteLine($"User3: Increase event выполнено. Новая длина: {length}");

            user1.Move(5);
            user2.Compress(0.5);
            user3.Move(-3);
            user3.Compress(0.8);

            Console.WriteLine($"User1: Позиция: {user1.Position}, Длина: {user1.Length}");
            Console.WriteLine($"User2: Позиция: {user2.Position}, Длина: {user2.Length}");
            Console.WriteLine($"User3: Позиция: {user3.Position}, Длина: {user3.Length}");

            var str = "HeLLo, Mr. Broun3";
            Func<string, string> a;
            a = Str.RemoveS;
            Console.WriteLine($"Без пунктуации:\n До: {str}\n После: {a(str)}\n");
            a = Str.RemoveSpase;
            Console.WriteLine($"Без пробелов:\n До: {str}\n После: {a(str)}\n");
            a = Str.Upper;
            Console.WriteLine($"Большие буквы:\n До: {str}\n После: {a(str)}\n");
            a = Str.Letter;
            Console.WriteLine($"Маленькие буквы:\n До: {str}\n После: {a(str)}\n");
            a = Str.AddToString;
            Console.WriteLine($"Добавление символов:\n До: {str}\n После: {a(str)}\n");
        }
    }
}