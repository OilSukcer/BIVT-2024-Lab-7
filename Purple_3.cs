using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _judge;

            public string Name => _name;
            public string Surname => _surname;
            public double[] Marks
            {
                get
                {
                    if (_marks == null) return default(double[]);
                    double[] marks = new double[_marks.Length];
                    Array.Copy(_marks, marks, marks.Length);
                    return marks;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return default(int[]);
                    int[] places = new int[_places.Length];
                    Array.Copy(_places, places, places.Length);
                    return places;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null) return default(int);
                    int score = 0;
                    foreach (int item in _places) score += item;
                    return score;
                }
            }

            private int TopPlace
            {
                get
                {
                    if (_places == null) return default(int);
                    int result = _places[0];
                    for (int i = 0; i < _places.Length; i++)
                        if (_places[i] < result) result = _places[i];
                    return result;
                }
            }

            private double TotalPoints
            {
                get
                {
                    if (_marks == null) return default(double);
                    double totalPoints = 0;
                    for (int i = 0; i < _marks.Length; i++)
                        totalPoints += _marks[i];
                    return totalPoints;
                }
            }
            public int Judge => _judge;
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _judge = 0;
            }

            public void Evaluate(double result)
            {
                if (_judge >= 7 || _marks == null) return;

                _marks[_judge] = result;
                _judge++;
            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;


                for (int judge = 0; judge < 7; judge++)
                {
                    for (int i = 1, j = 2; i < participants.Length;)
                    {
                        if (i != 0 && (participants[i - 1]._marks == null || participants[i]._marks == null)) continue;

                        if (i == 0 || participants[i - 1]._marks[judge] > participants[i]._marks[judge])

                        {
                            i = j;
                            j++;
                        }
                        else
                        {
                            var temp = participants[i - 1];
                            participants[i - 1] = participants[i];
                            participants[i] = temp;
                            i--;
                        }
                    }
                    for (int i = 0; i < participants.Length; i++)
                    {
                        if (participants[i]._places == null) continue;
                        participants[i]._places[judge] = i + 1;
                    }
                }

            }
            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                Participant[] participants = array.Where(arr => arr._places != null && arr._marks != null).ToArray();
                participants = participants.OrderBy(participant => participant.Score).ThenBy(participant => participant.TopPlace)
                    .ThenByDescending(participant => participant.TotalPoints).ToArray();

                //array = participants;
                Array.Copy(participants, array, participants.Length);
            }
            public void Print()
            {
                Console.WriteLine("Name: " + _name);
                Console.WriteLine("Surname: " + _surname);
                Console.WriteLine("Places:");

                foreach (double item in _places)
                {
                    Console.Write($"{item,4}");
                }
                Console.WriteLine();



            }
        }

        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;

            public Participant[] Participants
            {
                get { return _participants; }
            }
            public double[] Moods
            {
                get
                {
                    if ( _moods == null ) return default(double[]);
                    double[] moods = new double[_moods.Length];
                    Array.Copy(_moods, moods, _moods.Length);
                    return moods;
                }
            }

            public Skating(double[] moods)
            {
                _participants = new Participant[0];
                if (moods.Length > 7)
                    Array.Copy(moods, _moods, 7);
                else
                    _moods = moods;
                ModificateMood();
            }

            protected abstract void ModificateMood();
            public void Evaluate(double[] marks)
            {
                if (marks == null || _participants == null) return;

                foreach (var participant in _participants)
                {
                    if (participant.Judge == 0)
                    {
                        for (int judge = 0; judge < 7; judge++)
                        {
                            participant.Evaluate(marks[judge]);
                        }
                        break;
                    }
                }
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
        }

        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                if (_moods == null) return;

                for (int i = 0; i < 7; i++)
                {
                    _moods[i] += (i + 1) / 10.0;
                }
            }
        }

        public class IceSkating : Skating
        {
            public IceSkating(double[] moods) : base(moods) { }

            protected override void ModificateMood()
            {
                if (_moods == null) return;

                for (int i = 0; i < 7; i++)
                {
                    _moods[i] *= (i + 1 + 100) / 100.0;
                }
            }
        }
    }
}
