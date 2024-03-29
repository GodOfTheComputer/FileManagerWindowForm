﻿using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerWindowForm
{
    public static class LoginFormStaticHandler
    {
        private static TextEdit gmailTextEdit;
        private static TextEdit passwordTextEdit;

        public static void Init(TextEdit _gmailTextEdit, TextEdit _passwordTextEdit)
        {
            gmailTextEdit = _gmailTextEdit;
            passwordTextEdit = _passwordTextEdit;
        }

        public static void CheckForSignIn()
        {
            if (gmailTextEdit.Text == "" || passwordTextEdit.Text == "") throw new Exception("Empty field/Filling error");
        }

        public static async Task<string> ValidateSignIn()
        {
            UrlParameterContainer parameters = new UrlParameterContainer();

            parameters.AddParameter("Gmail", gmailTextEdit.Text, false);
            parameters.AddParameter("Password", passwordTextEdit.Text, false);

            return await HttpGenerator.GenerateJsonHttp("SignIn", parameters);
        }
    }
}
