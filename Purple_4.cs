using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            private bool _isNoTime = true;

            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
            }

            public void Run(double time)
            {
                if (!_isNoTime) return;
                _time = time;
                _isNoTime = false;
            }

            public void Print()
            {
                Console.WriteLine("Name: " + _name);
                Console.WriteLine("Surname: " + _surname);
                Console.WriteLine("time:" + _time);
            }

            public static void Sort(Sportsman[] array)
            {
                if (array == null) return;

                array = array.OrderBy(a => a.Time).ToArray();
            }
        }

        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }
            public SkiMan(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }
        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }
            public SkiWoman(string name, string surname, double time) : base(name, surname)
            {
                Run(time);
            }
        }

        public class Group
        {
            private string _name;
            private Sportsman[] _sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null) return default(Sportsman[]);
                    return _sportsmen;
                }
            }

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group group)
            {
                _name = group.Name;
                if (group.Sportsmen != null)
                {
                    _sportsmen = new Sportsman[group._sportsmen.Length];
                    Array.Copy(group._sportsmen, _sportsmen, group._sportsmen.Length);

                }
                else
                    _sportsmen = null;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null) return;

                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;
            }
            public void Add(Sportsman[] sportsmen)
            {
                if (_sportsmen == null || sportsmen == null) return;
                int ind = _sportsmen.Length;
                Array.Resize(ref _sportsmen, _sportsmen.Length + sportsmen.Length);

                for (int i = 0, j = 0; i < _sportsmen.Length; i++)
                {
                    if (i >= ind) _sportsmen[i] = sportsmen[j++];
                }
            }
            public void Add(Group group)
            {
                if (group._sportsmen == null) return;
                this.Add(group._sportsmen);
            }

            public void Sort()
            {
                if (_sportsmen == null) return;

                _sportsmen = _sportsmen.OrderBy(s => s.Time).ToArray();
            }
            public static Group Merge(Group group1, Group group2)
            {
                Group MergedGroup = new Group("Финалисты");

                MergedGroup.Add(group1);
                MergedGroup.Add(group2);
                MergedGroup.Sort();
                return MergedGroup;
            }

            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = null; women = null;
                if (_sportsmen == null) return ;

                men = _sportsmen.Where(s => s is SkiMan).ToArray();
                women = _sportsmen.Where(s => s is SkiWoman).ToArray();
            }

            public void Shuffle()
            {
                if (_sportsmen == null) return;

                Sort();

                

                Split(out Sportsman[] men, out Sportsman[] women);

                

                if (men == null || women == null || men.Length == 0 || women.Length == 0) return;

                Sportsman.Sort(men);
                Sportsman.Sort(women);

                

                Sportsman[] mergedSportsmen = Merge(men, women);

                

                _sportsmen = mergedSportsmen == null ? _sportsmen : mergedSportsmen;
            }

            private Sportsman[] Merge(Sportsman[] men, Sportsman[] women)
            {
                if (men == null || women == null || (men.Length == 0 && women.Length == 0)) return null;
                if (men.Length == 0) return women;
                if (women.Length == 0) return men;
                
                int m = 0, w = 0, s = 0;
                Sportsman[] sportsmen = new Sportsman[men.Length + women.Length];

                while (m < men.Length && w < women.Length)
                {
                    sportsmen[s++] = men[0].Time < women[0].Time ? men[m++] : women[w++];
                    sportsmen[s++] = men[0].Time < women[0].Time ? women[w++] : men[m++];
                }

                if (men.Length > women.Length)
                {
                    
                    while (m < men.Length)
                        sportsmen[s++] = men[m++];
                }
                else
                {
                    while (w < women.Length)
                        sportsmen[s++] = women[w++];
                }

                return sportsmen;
            }

            public void Print()
            {
                Console.WriteLine("Name: " + _name);
                Console.WriteLine("Sportsmen:");

                foreach (Sportsman item in _sportsmen)
                {
                    Console.Write($"{item.Name,4} {item.Surname,4} {item.Time}");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
