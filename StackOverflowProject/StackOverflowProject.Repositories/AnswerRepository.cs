using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IAnswerRepository
    {
        void InsertAnswer(Answer a);

        void UpdateAnswer(Answer a);
        void UpdateAnswerVotesCount(int aid, int uid,int value);

        void DeleteAnswer(int AnswerID);

        List<Answer> GetAnswersByQuestion(int qid);
        List<Answer> GetAnswersByAnswerID(int AnswerID);
    }

    public class AnswerRepository
    {
        StackOverflowDBContext db = new StackOverflowDBContext();
        IQuestionRepository qr = new QuestionRepository();
        IVoteRepository vr = new VoteRepository();


        public void DeleteAnswer(int AnswerID)
        {
            Answer answerToDelete = db.Answers.Where(a => a.AnswerID == AnswerID).FirstOrDefault();
            if (answerToDelete != null)
            {
                db.Answers.Remove(answerToDelete);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(answerToDelete.QuestionID, -1);
            }
        }

        public List<Answer> GetAnswersByQuestion(int qid)
        {
            List<Answer> Answers = db.Answers.Where(a=>a.QuestionID == qid).OrderByDescending(a => a.AnswerDateAndTime).ToList();
            return Answers;
        }

        public List<Answer> GetAnswersByAnswerID(int AnswerID)
        {
            List<Answer> Answers = db.Answers.Where(a => a.AnswerID == AnswerID).ToList();
            return Answers;
        }

        public void InsertAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(a.QuestionID, 1);
        }

        public void UpdateAnswer(Answer a)
        {
            Answer answerToUpdate = db.Answers.Where(temp => temp.AnswerID == a.AnswerID).FirstOrDefault();
            if (answerToUpdate != null)
            {
                answerToUpdate.AnswerText = a.AnswerText;
                answerToUpdate.AnswerDateAndTime = a.AnswerDateAndTime;
                db.SaveChanges();
            }
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer answerToUpdate = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if (answerToUpdate != null)
            {
                answerToUpdate.VotesCount += value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(answerToUpdate.QuestionID, value);
                vr.UpdateVote(aid, uid, value);
            }
        }        
    }
}
