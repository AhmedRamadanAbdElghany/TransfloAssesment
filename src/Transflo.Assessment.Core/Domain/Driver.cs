namespace Transflo.Assessment.Core.Domain
{
    public class Driver
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public static Driver Create( string firstName, string lastName, string phoneNumber ,string emailAddress )
        {
            if (string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(firstName))
                throw new InvalidOperationException("Invalid Data while create Driver");

            return new Driver
            {
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                PhoneNumber = phoneNumber.Trim(),
                EmailAddress = emailAddress.Trim()
            };
        }
        public static Driver Update(Driver driver, string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            if (driver.Id <= 0)
                throw new InvalidOperationException("Invalid Data while updat Driver");

            driver.FirstName = firstName.Trim();
            driver.LastName = lastName.Trim();
            driver.PhoneNumber = phoneNumber.Trim();
            driver.EmailAddress = emailAddress.Trim();
            return driver;


        }
    }
}
