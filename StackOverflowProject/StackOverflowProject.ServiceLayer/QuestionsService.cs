using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;
using StackOverflowProject.Repositories;
using StackOverflowProject.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;


namespace StackOverflowProject.ServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel qvm);

        void UpdateQuestionDetails(EditQuestionsViewModel qvm);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);

        void DeleteQuestion(int QuestionID);

        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionsByQuestionID(int QuestionID,int UserID);
    }

    public class QuestionsService:IQuestionsService
    {
        IQuestionRepository qr;

        public QuestionsService()
        {
            qr = new QuestionRepository();
        }

        public void InsertQuestion(NewQuestionViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewQuestionViewModel, Question>(); cfg.IgnoreUnmapped();
                cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped();
                cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();

            Question c = mapper.Map<NewQuestionViewModel, Question>(qvm);

            qr.InsertQuestion(c);
        }

        public void UpdateQuestionDetails(EditQuestionsViewModel qvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditQuestionsViewModel, Question>(); cfg.IgnoreUnmapped();
                cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped();
                cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();

            Question u = mapper.Map<EditQuestionsViewModel, Question>(qvm);

            qr.UpdateQuestionDetails(u);
        }

        public void UpdateQuestionVotesCount(int qid, int value)
        {
            qr.UpdateQuestionVotesCount(qid, value);
        }

        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            qr.UpdateQuestionAnswersCount(qid, value);
        }


        public void UpdateQuestionViewsCount(int qid, int value)
        {
            qr.UpdateQuestionViewsCount(qid, value);
        }


        public void DeleteQuestion(int QuestionID)
        {
            qr.DeleteQuestion(QuestionID);
        }

        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> q = qr.GetQuestions();

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped();
                cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped();
                cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped();
            });

            IMapper mapper = config.CreateMapper();

            List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(q);

            return qvm;
        }

        public QuestionViewModel GetQuestionsByQuestionID(int QuestionID,int userID = 0)
        {
            Question q = qr.GetQuestionsByQuestionID(QuestionID).FirstOrDefault();

            QuestionViewModel qvm = null;
            if (q != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped();
                    cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped();
                    cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped();
                });

                IMapper mapper = config.CreateMapper();

                qvm = mapper.Map<Question, QuestionViewModel>(q);

                foreach(var item in qvm.Answers)
                {
                    item.CurrentUserVoteType = 0;
                    VoteViewModel vote = item.Votes.Where(temp => temp.UserID == userID).FirstOrDefault();
                    if(vote!=null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }
                }
                return qvm;
            }
            return qvm;
        }
    }
}
