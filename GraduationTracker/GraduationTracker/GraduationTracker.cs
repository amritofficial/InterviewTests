using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        public Tuple<bool, STANDING> HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;
            var TotalCourses = 0;

            for (int i = 0; i < diploma.Requirements.Length; i++)
            {
                Course[] courses = student.Courses.Where(course => diploma.Requirements[i].Courses.Contains(course.Id)).ToArray<Course>();
                if (courses.Count() == 0)
                {
                    return new Tuple<bool, STANDING>(false, STANDING.Remedial);
                }

                average += courses.Select(course => course.Mark).Sum();
                int coursesPassed = courses.Where(course => course.Mark > diploma.Requirements[i].MinimumMark).Count();
                credits += coursesPassed * diploma.Requirements[i].Credits;
                TotalCourses += courses.Count();
            }

            average = average / TotalCourses;

            if (credits < diploma.Credits)
            {
                return new Tuple<bool, STANDING>(false, STANDING.Remedial);
            }

            var standing = STANDING.None;

            if (average < 50)
                standing = STANDING.Remedial;
            else if (average < 80)
                standing = STANDING.Average;
            else if (average < 95)
                standing = STANDING.MagnaCumLaude;
            else
                standing = STANDING.MagnaCumLaude;


            switch (standing)
            {
                case STANDING.Remedial:
                    return new Tuple<bool, STANDING>(false, standing);
                case STANDING.Average:
                    return new Tuple<bool, STANDING>(true, standing);
                case STANDING.SumaCumLaude:
                    return new Tuple<bool, STANDING>(true, standing);
                case STANDING.MagnaCumLaude:
                    return new Tuple<bool, STANDING>(true, standing);

                default:
                    return new Tuple<bool, STANDING>(false, standing);
            }
        }
    }
}
