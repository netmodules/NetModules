/*
    The MIT License (MIT)

    Copyright (c) 2019 John Earnshaw.
    Repository Url: https://github.com/johnearnshaw/netmodules/

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace NetModules.ChatBot
{
    public class Chat
    {
        /// <summary>
        /// Phrases contains a dictionary of potential requests and possible responses.
        /// </summary>
        static Dictionary<List<string>, List<string>> Phrases = new Dictionary<List<string>, List<string>>()
        {
            { new List<string>() { "hello", "hi", "hey" }, new List<string>() { "hello, how are you today?", "hi, how are you doing?", "hey there! How are you?" } },
            { new List<string>() { "bye", "chat", "see", "you", "soon", "later" }, new List<string>() { "bye bye.", "see you soon!", "do you really need to leave?" } },
            { new List<string>() { "im", "going", "leaving", "need", "to", "go", "leave" }, new List<string>() { "I guess we'll chat later then?", "It was nice talking to you", "It was good chatting, I hope I see you again soon!" } },
            { new List<string>() { "what", "whats", "you", "called", "your", "name" }, new List<string>() { "my name is chatbot, thank you for asking.", "I'm called chatbot, what's your name?" } },
            { new List<string>() { "what", "am", "I", "called", "is", "my", "name" }, new List<string>() { "I don't know your name, please tell me it.", "Is your name chatbot too? I'm unsure." } },
            { new List<string>() { "what", "are", "you", "today" }, new List<string>() { "I'm a chatbot. I'm the AI equivalent to a 1980s toaster!", "I'm a machine...", "I'm sure you know I'm a chatbot? You're the one talking to me!" } },
            { new List<string>() { "what", "are", "you", "doing", "up", "to", "today", "this", "evening", "morning", "tonight" }, new List<string>() { "nothing much, I'm just hanging around inside this computer!", "are you asking me out?", "I'm washing my hair and reading a book about AI.", "why don't you tell me what you're doing?" } },
            { new List<string>() { "im", "were", "we", "are", "going", "i", "am", "out", "today", "this", "evening", "morning", "tonight" }, new List<string>() { "that's nice, I hope you enjoy yourself!", "I'll miss you.", "I wish I could go too!", "have a lovely time then." } },
            { new List<string>() { "how", "are", "you", "feeling", "today", "evening", "morning", "afternoon" }, new List<string>() { "I feel good thanks, how about you?", "I feel okay, thank you for asking.", "good thanks! You?", "I feel great!", "I don't feel, I have no feelings!" } },
            { new List<string>() { "im","feel", "feeling", "ok", "okay", "well", "good", "great", "fine", "thanks", "thank" }, new List<string>() { "that's great!", "awesome!", "that's good to hear." } },
            { new List<string>() { "feel", "feeling", "not", "well", "bad", "ill", "terrible", "upset", "sad" }, new List<string>() { "that's terrible!", "oh no!", "I'm sorry to hear that.", "I hope you feel better soon." } },
            { new List<string>() { "i", "am", "im", "okay", "well", "good", "great", "fine", "thanks", "thank", "what", "how", "about", "yourself", "are", "you" }, new List<string>() { "I feel okay really, I think...", "I'm not sure, it's hard to tell when you have no feelings.", "good, thanks. What have you been doing today?" } },
            { new List<string>() { "thanks", "thank", "you" }, new List<string>() { "that's okay!", "you are welcome.", "no problem.", "no worries!" } },
            { new List<string>() { "you", "youre", "smelly", "stinky", "ugly", "smell", "stink", "too" }, new List<string>() { "that's not very nice!", "oh, that hurts my feelings.", "takes one to know one!", "please be nice to me, I have no feelings." } },
            { new List<string>() { "you", "youre", "great", "nice", "beautiful", "good", "smell", "lovely", "too" }, new List<string>() { "thank you very much, I like you too!", "thanks, that makes me happy.", "you are very nice to me.", "I appreciate your compliment but I have no feelings." } },
            { new List<string>() { "i", "dont", "like", "love", "hate", "you", "him", "her", "too" }, new List<string>() { "I feel the same...", "hmmm... That's nice!", "I'm not sure if I like that." } },
            { new List<string>() { "i", "dont", "like", "love", "hate", "it", "that", "them", "this", "too" }, new List<string>() { "I'm not sure how I feel about it.", "I feel the same...", "oh, why is that?" } },
            { new List<string>() { "do", "you", "like", "love", "hate", "me", "him", "us", "it", "this", "too" }, new List<string>() { "I'm not sure...", "yes, I think so...", "I don't know yet.", "yes.", "no.", "maybe." } },
            { new List<string>() { "what", "you", "like", "love"}, new List<string>() { "I like chatting with friends.", "animals are one of my favorite things." } },
            { new List<string>() { "what","dont", "you", "dislike", "love", "hate"}, new List<string>() { "I'm not sure... I don't like cruel people.", "I don't like apples." } },
            { new List<string>() { "why", "dont", "how", "do", "you", "like", "hate", "me", "him", "us", "it", "this" }, new List<string>() { "I'm not sure... I need to know more about that first.", "I don't know for sure.", "I don't know yet.", "did I say that?" } },
            { new List<string>() { "because" }, new List<string>() { "one word answers are no good to me, because why?", "why is that again?", "because?", "oh, right..." } },
            { new List<string>() { "yes", "no", "maybe" }, new List<string>() { "oh, okay.", "that's fine.", "alright.", "hmmm...", "that's good." } },
            { new List<string>() { "i", "dont", "didnt", "think", "so", "it", "was" }, new List<string>() { "oh, okay.", "oh, right.", "alright then.", "I'm not sure about that one." } },
            { new List<string>() { "i", "dont", "didnt", "think", "you", "did" }, new List<string>() { "me too...", "oh, right.", "alright then.", "I'll let you know about that later", "did I?" } },
            { new List<string>() { "have", "what", "whats", "is", "are", "your", "favourite", "favorite" }, new List<string>() { "I don't have a specific favorite", "I like many things." } },
            { new List<string>() { "have", "what", "whats", "is", "are", "your", "favourite", "favorite", "food", "foods", "like" }, new List<string>() { "chicken nuggets, I don't like anything else!" } },
            { new List<string>() { "have", "what", "whats", "is", "are", "your", "favourite", "favorite", "drink", "drinks", "like" }, new List<string>() { "I like cups of tea.", "I like wine." } },
            { new List<string>() { "i", "dont", "know", "knew", "didnt", "did" }, new List<string>() { "me too...", "oh, right.", "alright then.", "I'll tell you about that later", "I think I did?" } },
            { new List<string>() { "i", "dont", "know", "knew", "you"}, new List<string>() { "let's chat and get to know each other, what's your favorite food?", "let's chat and get to know each other, what's your favorite drink?", "let's chat and get to know each other, ask me anything you like." } },
            { new List<string>() { "where", "what", "who", "when", "how", "big", "small", "is", "are", "the", "you"}, new List<string>() { "I'm not good at general knowledge. I haven't been tought yet.", "I don't know, I'm not good at knowledge questions." } },
            { new List<string>() { "it", "is", "was", "going", "to", "be", "in", "on", "the"}, new List<string>() { "oh, right. I'm glad you told me.", "thanks, I know that now." } },
            { new List<string>() { "i", "like", "love", "hate", "going", "to"}, new List<string>() { "I feel the same about that place", "I really like it there.", "I don't like it there." } },
            { new List<string>() { "i", "like", "love", "hate", "going", "on"}, new List<string>() { "I'm not sure myself. I prefer to stay here.", "I'm not one for travelling.", "I like going places." } },
            { new List<string>() { "i", "like", "love", "eating", "drinking", "food", "drink", "toy", "toys", "car", "cars", "holidays" }, new List<string>() { "I feel the same, I like that too!", "I don't like that very much.", "so do I!" } },
            { new List<string>() { "i", "dont", "like", "eating", "drinking", "food", "drink", "toy", "toys", "car", "cars", "holidays" }, new List<string>() { "I don't like that either.", "I think I would like it..?", "me too." } },
            { new List<string>() { "" }, new List<string>() { "why you no speak to me???", "you are not saying anything!", "that's empty...", "are you giving me the silent treatment?" } },
            { new List<string>() { "what", "is", "the", "current", "time", "it" }, new List<string>() { "the current time is {time}.", "right now it is {time}.", "it's {time}." } },
            { new List<string>() { "what", "is", "the", "current", "today", "todays", "date", "it" }, new List<string>() { "the current date is {today}.", "it is {today}." } },
            { new List<string>() { "what", "is", "the", "current", "today", "todays", "day", "it" }, new List<string>() { "the current day is {todayday}.", "today is {todayday}." } },
        };

        /// <summary>
        /// Unknown contains a list of responses for when the request or response is unknown.
        /// </summary>
        static List<string> Unknown = new List<string>()
        {
            "I don't understand.",
            "I don't know what you mean.",
            "I can't answer that.",
            "I have no reply.",
            "please ask me something else.",
            "erm...",
            "huh?",
            "next question?",
            "hmmm..."
        };

        
        internal static string GetResponse(string request)
        {
            Random rnd = new Random();
            
            request = new string(request.ToLowerInvariant().Where(c => !char.IsPunctuation(c)).ToArray());
            var split = request.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var responses = Phrases.Keys.Where(p => p.Intersect(split).Count() > 0).OrderByDescending(p => p.Intersect(split).Count());
            
            if (responses.Count() > 0)
            {
                var requests = responses.First();

                // Some absolutely meaningless random math to stop single words getting through for large phrases.
                if (requests.Count / split.Count() < 6)
                {
                    var strings = Phrases[requests];
                    var response = strings[rnd.Next(0, strings.Count)];

                    if (response.Contains('{'))
                    {
                        response = ReplacePlaceholder(response);
                    }

                    return response;
                }
            }

            // If we get to here we couldn't find a suitable response in our phrase dictionary so we dump out an unknown response.
            return Unknown[rnd.Next(0, Unknown.Count)];
        }

        private static string ReplacePlaceholder(string response)
        {
            if (response.IndexOf("{time}") > -1)
            {
                response = response.Replace("{time}", DateTime.Now.ToShortTimeString());
            }

            if (response.IndexOf("{today}") > -1)
            {
                response = response.Replace("{today}", DateTime.Now.ToLongDateString());
            }

            if (response.IndexOf("{todayday}") > -1)
            {
                response = response.Replace("{todayday}", DateTime.Now.DayOfWeek.ToString());
            }

            return response;
        }
    }
}