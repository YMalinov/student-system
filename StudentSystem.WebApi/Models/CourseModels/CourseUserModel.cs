using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StudentSystem.WebApi.Models
{
    [DataContract(Name="coursemodel")]
    public class CourseUserModel
    {
        [DataMember(Name="courseid")]
        public int? Id { get; set; }

        [DataMember(Name = "coursetitle")]
        public string Title { get; set; }

        [DataMember(Name = "submitedHomewroksCount")]
        public int SubmitedHomeworksCount { get; set; }

        [DataMember(Name = "HomewroksCount")]
        public int HomeworksCount { get; set; }

        [DataMember(Name = "testScore")]
        public int TestScore { get; set; }

        [DataMember(Name = "examScore")]
        public int ExamScore { get; set; }

        [DataMember(Name = "examScoreMax")]
        public int ExamScoreMax { get; set; }

        [DataMember(Name = "finalResult")]
        public FinalResultModel FinalResult { get; set; }
    }
}