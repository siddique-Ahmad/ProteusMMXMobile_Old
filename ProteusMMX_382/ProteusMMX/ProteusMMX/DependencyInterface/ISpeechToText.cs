using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.DependencyInterface
{
    public interface ISpeechToText
    {
        Task<string> SpeechToText();
    }
}
