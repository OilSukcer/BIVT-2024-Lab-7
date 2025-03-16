using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _result;

            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return default(int[]);
                    int[] marks = new int[_marks.Length];
                    Array.Copy(_marks, marks, marks.Length);
                    return marks;
                }
            }
            public int Result => _result;

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _result = 0;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || _marks == null) return;

                _distance = distance;
                for (int i = 0; i < _marks.Length; i++) _marks[i] = marks[i];

                int sum = 0, min = 21, max = 0;

                for (int i = 0; i < _marks.Length; i++)
                {
                    if (_marks[i] < min) min = _marks[i];
                    if (_marks[i] > max) max = _marks[i];
                    sum += _marks[i];
                }
                sum -= max;
                sum -= min;
                if (_distance > target)
                    sum += (60 + (_distance - target) * 2);
                else
                    sum = sum + 60 - (target - _distance) * 2;

                if (sum < 0) sum = 0;
                _result = sum;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i - 1].Result >= array[i].Result)
                    {
                        i = j;
                        j++;
                        Console.WriteLine(1);
                    }
                    else
                    {
                        var temp = array[i - 1];
                        array[i - 1] = array[i];
                        array[i] = temp;
                        i--;
                    }
                }

            }
            public void Print()
            {
                Console.WriteLine("Name: " + _name);
                Console.WriteLine("Surname: " + _surname);
                Console.WriteLine("Marks:");

                foreach (double item in _marks)
                {
                    Console.Write($"{item,4}");
                }
                Console.WriteLine();



            }
        }

        public class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants
            {
                get { return _participants; }
            }

            public SkiJumping(string name, int standart)
            {
                _name = name;
                _standard = standart;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (_participants == null || participants == null) return;
                Array.Resize(ref _participants, _participants.Length + participants.Length);
                for (int i = _participants.Length - participants.Length, j = 0; i < _participants.Length; i++)
                    _participants[i] = participants[j++];
            }
            public void Jump(int distance, int[] marks)
            {
                if ( marks == null || _participants == null) return;

                foreach (var participant in _participants)
                {
                    if (participant.Distance == 0)
                    {
                        participant.Jump(distance, marks, _standard);
                        break;
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine("Name: " + _name);

                Console.WriteLine("Standard: " + _standard);
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}
