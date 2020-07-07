using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IQuestionRepository
    {
        void InsertQuestion(Question q);

        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);

        void DeleteQuestion(int QuestionID);

        List<Question> GetQuestions();
        List<Question> GetQuestionsByQuestionID(int QuestionID);
    }

    public class QuestionRepository:IQuestionRepository
    {
        StackOverflowDBContext db = new StackOverflowDBContext();

        public void DeleteQuestion(int QuestionID)
        {
            Question questionToDelete = db.Questions.Where(q => q.QuestionID == QuestionID).FirstOrDefault();
            if (questionToDelete != null)
            {
                db.Questions.Remove(questionToDelete);
                db.SaveChanges();
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> Questions = db.Questions.OrderByDescending(q =>q.QuestionDateAndTime).ToList();
            return Questions;
        }

        public List<Question> GetQuestionsByQuestionID(int QuestionID)
        {
            List<Question> qt = db.Questions.Where(q => q.QuestionID == QuestionID).ToList();
            return qt;
        }

        public void InsertQuestion(Question q)
        {
            db.Questions.Add(q);
            db.SaveChanges();
        }

        public void UpdateQuestionDetails(Question q)
        {
            Question questionToUpdate = db.Questions.Where(temp => temp.QuestionID == q.QuestionID).FirstOrDefault();
            if (questionToUpdate != null)
            {
                questionToUpdate.QuestionName = q.QuestionName;
                questionToUpdate.CategoryID = q.CategoryID;
                questionToUpdate.QuestionDateAndTime = q.QuestionDateAndTime;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            Question questionToUpdate = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (questionToUpdate != null)
            {
                questionToUpdate.VotesCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            Question questionToUpdate = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (questionToUpdate != null)
            {
                questionToUpdate.AnswersCount += value;
                db.SaveChanges();
            }
        }

        public void UpdateQuestionViewsCount(int qid, int value)
        {
            Question questionToUpdate = db.Questions.Where(temp => temp.QuestionID == qid).FirstOrDefault();
            if (questionToUpdate != null)
            {
                questionToUpdate.ViewsCount += value;
                db.SaveChanges();
            }
        }
        
    }
}
