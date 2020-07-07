using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Mvc5;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.Repositories;
using System.Web.Mvc;

namespace StackOverflowProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            container.RegisterType<IAnswerRepository, AnswerRepository>();
            container.RegisterType<ICategoryRepository,CategoryRepository>();
            container.RegisterType<IQuestionRepository,QuestionRepository>();
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IVoteRepository,VoteRepository>();

            container.RegisterType<IQuestionsService, QuestionsService>();
            container.RegisterType<IAnswerService, AnswerService>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<ICategoryService, CategoryService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}