using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;

namespace StackOverflowProject.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionsService qs;
        IAnswerService ias;
        ICategoryService cs;

        public QuestionsController(IQuestionsService Qs,IAnswerService Ias,ICategoryService Cs)
        {
            this.qs = Qs;
            this.ias = Ias;
            this.cs = Cs;
        }
        // GET: Questions
        public ActionResult Views(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.qs.GetQuestionsByQuestionID(id, uid);
            return View(qvm);
        }
    }
}