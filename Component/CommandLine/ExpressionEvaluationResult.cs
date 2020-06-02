﻿using DotNetConsoleSdk.Component.CommandLine.Parsing;
using System;

namespace DotNetConsoleSdk.Component.CommandLine
{
    public class ExpressionEvaluationResult
    {
        public readonly string SyntaxError;
        public readonly object Result;
        public readonly int EvalResultCode;
        public readonly ParseResultType ParseResult;
        public readonly Exception EvalError;

        public ExpressionEvaluationResult(
            string syntaxError,
            ParseResultType parseResult, 
            object result, 
            int evalResultCode,
            Exception evalError)
        {
            SyntaxError = syntaxError;
            ParseResult = parseResult;
            Result = result;
            EvalResultCode = evalResultCode;
            EvalError = evalError;
        }
    }
}