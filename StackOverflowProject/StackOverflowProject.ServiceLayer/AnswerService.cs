using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;
using StackOverflowProject.Repositories;
using StackOverflowProject.ViewModels;
using AutoMapper;

namespace StackOverflowProject.ServiceLayer
{
    public interface IAnswerService
    {
        void InsertAnswer(NewAnswerViewModel avm);

        void UpdateAnswer(EditAnswerViewModel avm);
        void UpdateAnswerVotesCount(int aid, int uid, int value);

        void DeleteAnswer(int AnswerID);

        List<AnswerViewModel> GetAnswersByQuestion(int qid);
        AnswerViewModel GetAnswersByAnswerID(int AnswerID);
    }

    public class AnswerService:IAnswerService
    {
        IAnswerRepository ar;

        public AnswerService()
        {
            ar = new AnswerRepository();
        }

        public void InsertAnswer(NewAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            Answer a = mapper.Map<NewAnswerViewModel, Answer>(avm);

            ar.InsertAnswer(a);
        }

        public void UpdateAnswer(EditAnswerViewModel avm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            Answer a = mapper.Map<EditAnswerViewModel, Answer>(avm);

            ar.UpdateAnswer(a);
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            ar.UpdateAnswerVotesCount(aid, uid, value);
        }

        public void DeleteAnswer(int AnswerID)
        {
            ar.DeleteAnswer(AnswerID);
        }

        public List<AnswerViewModel> GetAnswersByQuestion(int qid)
        {
            List<Answer> u = ar.GetAnswersByQuestion(qid);

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });

            IMapper mapper = config.CreateMapper();

            List<AnswerViewModel> avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(u);

            return avm;
        }

        public AnswerViewModel GetAnswersByAnswerID(int AnswerID)
        {
            Answer a = ar.GetAnswersByAnswerID(AnswerID).FirstOrDefault();

            AnswerViewModel avm = null;
            if (a != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });

                IMapper mapper = config.CreateMapper();

                avm = mapper.Map<Answer, AnswerViewModel>(a);

                return avm;
            }
            return avm;
        }
    }
}
