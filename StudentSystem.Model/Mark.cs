using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Model
{
    public class Mark
    {
        public int Id { get; set; }
        public decimal Score { get; set; }
        public int TestScore { get; set; }
        public int ExamScore { get; set; }
        public CourseFinalResultType FinalResult { get; set; }
        public int Position { get; set; }
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
