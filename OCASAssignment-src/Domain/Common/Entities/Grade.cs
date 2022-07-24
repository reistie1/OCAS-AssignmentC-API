namespace OCAS.Domain.Common
{
    public class Grade : BaseEntity
    {
        public Guid CourseId {get; set;}
        public virtual Course Course {get; set;}
        public Guid StudentId {get; set;}
        public virtual Student Student {get; set;}
        public int NumericGrade {get; set;}
        public int AlphabeticGrade {get; set;}
    }
}
