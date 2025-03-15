using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_1
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _count;
            private double _totalScore;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return default(double[]);
                    double[] coefs = new double[_coefs.Length];
                    Array.Copy(_coefs, coefs, coefs.Length);
                    return coefs;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return default(int[,]);
                    int[,] marks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++) marks[i, j] = _marks[i, j];
                    }
                    return marks;
                }
            }

            public double TotalScore
            {
                get
                {
                    if (_marks == null || _coefs == null || _count < 4) return default(double);
                    double total = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        int max = 0, min = 7, sum = 0;
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            if (_marks[i, j] > max) max = _marks[i, j];
                            if (_marks[i, j] < min) min = _marks[i, j];
                            sum += _marks[i, j];
                        }
                        sum -= min;
                        sum -= max;
                        total += sum * _coefs[i];
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _count = 0;
                _totalScore = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || _coefs == null) return;
                for (int i = 0; i < coefs.Length; i++) _coefs[i] = coefs[i];
            }
            public void Jump(int[] marks)
            {
                if (marks == null || _marks == null || _count >= 4) return;

                for (int j = 0; j < marks.Length; j++) _marks[_count, j] = marks[j];
                _count++;
                if (_count == 4)
                {
                    if (_marks == null) return;
                    double total = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        int max = 0, min = 7, sum = 0;
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            if (_marks[i, j] > max) max = _marks[i, j];
                            if (_marks[i, j] < min) min = _marks[i, j];
                            sum += _marks[i, j];
                        }
                        sum -= min;
                        sum -= max;
                        total += sum * _coefs[i];
                    }
                    this._totalScore = total;
                }
            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 1, j = 2; i < array.Length;)
                {
                    if (i == 0 || array[i - 1].TotalScore >= array[i].TotalScore)
                    {
                        i = j;
                        j++;
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
                Console.WriteLine("Coefs:");

                foreach (double item in _coefs)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();

                Console.WriteLine("Marks:");
                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++) Console.Write($"{_marks[i, j],3}");
                    Console.WriteLine();
                }
                Console.WriteLine("Total score: " + _totalScore);


            }
        }

        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _count;

            public string Name => _name;
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
            public Judge(string name, int[] marks)
            {
                _name = name;
                _marks = marks;
                _count = 0;
            }

            public int CreateMark()
            {
                if (_marks == null) return default(int);

                if (_count >= _marks.Length) _count = 0;
                return _marks[_count++];
            }

            public void Print()
            {
                Console.WriteLine("Judge");
                Console.WriteLine("Name: " + _name);
                Console.WriteLine("Marks:");

                for (int j = 0; j < _marks.GetLength(1); j++) Console.Write($"{_marks[j],3}");
                Console.WriteLine();

            }
        }
        public class Competition
        {
            private Participant[] _participants;
            private Judge[] _judges;

            public Participant[] Participants
            {
                get { return _participants; }
            }
            public Judge[] Judges
            {
                get { return _judges; }
            }

            public Competition(Judge[] judges)
            {
                _participants = new Participant[0];
                _judges = judges;
            }

            public void Evaluate(Participant jumper) 
            {
                if ( _judges == null ) return;

                int[] marks = new int[7];
                int i = 0;
                foreach (Judge judge in _judges )
                {
                    if (i >=  marks.Length) break;
                    marks[i++] = judge.CreateMark();
                }

                jumper.Jump(marks);
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
                for (int i = _participants.Length - participants.Length - 1, j = 0; i < _participants.Length; i++)
                    _participants[i] = participants[j++];
            }

            public void Sort()
            {
                if (_participants == null) return;

                Participant.Sort(_participants);
            }
        }
    }
}
