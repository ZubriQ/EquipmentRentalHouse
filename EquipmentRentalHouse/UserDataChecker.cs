using System.Text.RegularExpressions;

namespace EquipmentRentalHouse
{
    public static class UserDataChecker
    {
        private static Regex _regex;

        public static bool CheckLogin(string login)
        {
            _regex = new Regex(@"^[A-Za-z0-9][\w]{2,16}$");
            return _regex.Match(login).Success;
        }

        public static bool CheckPassword(string password)
        {
            var pattern = @"^[\w\~\`\!\@\#\$\%\^\&\*\(\)\-\+\="+ 
                    @"\{\[\}\]\|\\\:\;\'\<\,\>\.\?\/\ ]{6,32}$";
            _regex = new Regex(pattern);
            return _regex.Match(password).Success;
        }

        public static bool CheckName(string name)
        {
            _regex = new Regex(@"^[A-Za-zА-Яа-я][A-Za-zА-Яа-я\.\'\ ]+$");
            return _regex.Match(name).Success;
        }

        public static bool CheckPatronymic(string patronymic)
        {
            if (string.IsNullOrWhiteSpace(patronymic))
                return true;
            _regex = new Regex(@"^[A-Za-zА-Яа-я][A-Za-zА-Яа-я\.\'\ ]+$");
            return _regex.Match(patronymic).Success;
        }

        public static bool CheckPassportCode(string code)
        {
            _regex = new Regex(@"^[\d]{4}$");
            return _regex.Match(code).Success;
        }

        public static bool CheckPassportNumber(string number)
        {
            _regex = new Regex(@"^[\d]{6}$");
            return _regex.Match(number).Success;
        }
    }
}