namespace Faqidy.Application.Exceptions
{
    public class NotFoundException : Exception 
    {
        public NotFoundException()
            :base("Not Found") { }
        
        public NotFoundException(string message)
            : base(message) { }

        public NotFoundException(string name , object key)
            :base($"The {name} with id: {key} is not found.") { }

        public NotFoundException(string message , Exception inner)
            :base(message , inner) { }
        

    }
}
