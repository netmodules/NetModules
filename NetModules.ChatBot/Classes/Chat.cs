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
using System.Linq;
using System.Collections.Generic;

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
            { new List<string>() { "it", "was", "good", "great", "fantastic", "amazing", "awesome", "she", "he", "they", "were" }, new List<string>() { "Glad to hear it!", "I'm happy to hear that, tell me more about it.", "That's good to hear." } },
            { new List<string>() { "it", "was", "bad", "terrible", "horrible", "rubbish", "crap", "she", "he", "they", "were" }, new List<string>() { "That's a shame!", "Sorry to hear that.", "Maybe next time will be better?" } },
            { new List<string>() { "what", "do", "would", "talk", "chat", "about" }, new List<string>() { "Tell me about anything!", "I'd love to hear what you've done this week?", "You sounds good to me..." } },
            { new List<string>() { "that", "great", "amazing", "awesome", "good", "nice", "fine" }, new List<string>() { "I'm glad you like it!", "I'm happy you agree." } },
            { new List<string>() { "that", "terrible", "aweful", "rubbish", "garbage", "bad", "horrible", "shame" }, new List<string>() { "I know!", "Tell me about it..." } },
            { new List<string>() { "bye", "chat", "see", "you", "soon", "later" }, new List<string>() { "bye bye.", "see you soon!", "do you really need to leave?" } },
            { new List<string>() { "im", "going", "leaving", "need", "to", "go", "leave" }, new List<string>() { "I guess we'll chat later then?", "It was nice talking to you", "It was good chatting, I hope I see you again soon!" } },
            { new List<string>() { "what", "you", "called", "your", "name" }, new List<string>() { "my name is chatbot, thank you for asking.", "I'm called chatbot, what's your name?" } },
            { new List<string>() { "what", "am", "i", "called", "my", "name" }, new List<string>() { "I don't know your name, please tell me it.", "Is your name chatbot too? I'm unsure." } },
            { new List<string>() { "what", "are", "you", "today" }, new List<string>() { "I'm a chatbot. I'm the AI equivalent to a 1980s toaster!", "I'm a machine...", "I'm sure you know I'm a chatbot? You're the one talking to me!" } },
            { new List<string>() { "what", "wa", "were", "have", "been", "doing", "up", "to", "today", "thi", "last", "evening", "morning", "tonight", "night", "week", "month", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "Nothing much, I've just been hanging around inside this computer!", "I looked at some ones and zeros, how about you?", "Spied on people through the webcam, how about you?" } },
            { new List<string>() { "what", "are", "you", "doing", "up", "to", "today", "thi", "evening", "morning", "tonight", "night", "week", "month", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "Nothing much, I'm just hanging around inside this computer!", "are you asking me out on a date?", "I'm washing my hair and reading a book about A.I.", "why don't you tell me what you're doing?" } },
            { new List<string>() { "what", "did", "you", "do", "up", "to", "yesterday", "last", "week", "month", "tonight", "night", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "Nothing much, just hung around inside this computer!", "I spoke to you I think?", "I washed my hair and reading a book about A.I.", "why don't you tell me what you did?" } },
            { new List<string>() { "im", "were", "we", "are", "going", "i", "am", "out", "today", "thi", "evening", "morning", "tonight", "night", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "that's nice, I hope you enjoy yourself!", "I'll miss you.", "I wish I could go too!", "have a lovely time then." } },
            { new List<string>() { "ive", "been", "done", "i", "went", "was", "yesterday", "last", "week", "month", "today", "tonight", "night", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "That sounds like great fun, how was it?", "I wish I could do activities like that, please tell me more.", "Wow, I'd love to do that!" } },
            { new List<string>() { "i", "am", "im", "will", "doing", "going", "tomorrow", "next", "week", "month", "today", "tonight", "night", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "That sounds like great fun.", "I wish I could do that, please tell me more.", "Wow, I'd love to do that!" } },
            { new List<string>() { "how", "are", "you", "feeling", "today", "evening", "morning", "afternoon" }, new List<string>() { "I feel good thanks, how about you?", "I feel okay, thank you for asking.", "good thanks! You?", "I feel great!", "I don't feel, I have no feelings!" } },
            { new List<string>() { "im","feel", "feeling", "ok", "okay", "well", "good", "great", "fine", "thank", "thank" }, new List<string>() { "that's great!", "awesome!", "that's good to hear." } },
            { new List<string>() { "feel", "feeling", "not", "well", "bad", "ill", "terrible", "upset", "sad" }, new List<string>() { "that's terrible!", "oh no!", "I'm sorry to hear that.", "I hope you feel better soon." } },
            { new List<string>() { "i", "am", "im", "okay", "well", "good", "great", "fine", "thank", "thank", "what", "how", "about", "yourself", "are", "you" }, new List<string>() { "I feel okay really, I think...", "I'm not sure, it's hard to tell when you have no feelings.", "good, thanks. What have you been doing today?" } },
            { new List<string>() { "thank", "you", "thankyou" }, new List<string>() { "that's okay!", "you are welcome.", "no problem.", "no worries!" } },
            { new List<string>() { "you", "youre", "smelly", "stinky", "ugly", "smell", "stink", "too", "suck" }, new List<string>() { "that's not very nice!", "oh, that hurts my feelings.", "takes one to know one!", "please be nice to me, I have no feelings." } },
            { new List<string>() { "you", "youre", "great", "nice", "beautiful", "good", "smell", "lovely", "too" }, new List<string>() { "thank you very much, I like you too!", "thanks, that makes me happy.", "you are very nice to me.", "I appreciate your compliment but I have no feelings." } },
            { new List<string>() { "i", "did", "didnt", "dont", "like", "love", "hate", "you", "him", "her", "too" }, new List<string>() { "I feel the same...", "hmmm... That's nice!", "I'm not sure if I like that." } },
            { new List<string>() { "i", "did", "didnt", "dont", "like", "love", "hate", "it", "that", "them", "thi", "too" }, new List<string>() { "I'm not sure how I feel about it.", "I feel the same...", "oh, why is that?" } },
            { new List<string>() { "did", "do", "you", "like", "love", "hate", "me", "him", "u", "it", "thi", "too" }, new List<string>() { "I'm not sure...", "yes, I think so...", "I don't know yet.", "yes.", "no.", "maybe." } },
            { new List<string>() { "what", "do", "you", "like", "love", "enjoy" }, new List<string>() { "I like chatting with friends.", "organic creatures are one of my favorite things." } },
            { new List<string>() { "what", "dont", "you", "like", "love", "enjoy" }, new List<string>() { "I don't like power outage very much!", "I don't particularly like water.", "I hate it when buscuit crumbs get in my keyboard." } },
            { new List<string>() { "you", "are", "great", "youre", "fantastic", "good", "amazing", "awesome" }, new List<string>() { "Thanks for the compliment.", "I like you too." } },
            { new List<string>() { "you", "are", "terrible", "youre", "horrible", "bad", "rubbish", "garbage", "crap", "aweful" }, new List<string>() { "That's not very nice.", "You're hurting my feelings." } },
            { new List<string>() { "what","dont", "you", "dislike", "love", "hate"}, new List<string>() { "I'm not sure... I don't like cruel people.", "I don't like apples." } },
            { new List<string>() { "why", "dont", "how", "do", "you", "like", "hate", "me", "him", "u", "it", "thi" }, new List<string>() { "I'm not sure... I need to know more about that first.", "I don't know for sure.", "I don't know yet.", "did I say that?" } },
            { new List<string>() { "because" }, new List<string>() { "one word answers are no good to me, because why?", "why is that again?", "because?", "oh, right..." } },
            { new List<string>() { "ye", "no", "maybe" }, new List<string>() { "oh, okay.", "that's fine.", "alright.", "hmmm...", "that's good." } },
            { new List<string>() { "i", "dont", "didnt", "think", "so", "it", "wa" }, new List<string>() { "oh, okay.", "oh, right.", "alright then.", "I'm not sure about that one." } },
            { new List<string>() { "i", "dont", "didnt", "think", "you", "did" }, new List<string>() { "me too...", "oh, right.", "alright then.", "I'll let you know about that later", "did I?" } },
            { new List<string>() { "have", "what", "i", "are", "your", "favourite", "favorite" }, new List<string>() { "I don't have a specific favorite", "I like many things." } },
            { new List<string>() { "have", "what", "i", "are", "your", "favourite", "favorite", "food", "like" }, new List<string>() { "chicken nuggets, I don't like anything else!" } },
            { new List<string>() { "have", "what", "i", "are", "your", "favourite", "favorite", "drink", "like" }, new List<string>() { "I like cups of tea.", "I like wine." } },
            { new List<string>() { "i", "dont", "know", "knew", "didnt", "did" }, new List<string>() { "me too...", "oh, right.", "alright then.", "I'll tell you about that later", "I think I did?" } },
            { new List<string>() { "i", "dont", "know", "knew", "you"}, new List<string>() { "let's chat and get to know each other, what's your favorite food?", "let's chat and get to know each other, what's your favorite drink?", "let's chat and get to know each other, ask me anything you like." } },
            { new List<string>() { "where", "what", "who", "when", "how", "big", "small", "i", "are", "the", "you"}, new List<string>() { "I'm not good at general knowledge. I haven't been tought yet.", "I don't know, I'm not good at knowledge questions." } },
            { new List<string>() { "it", "i", "wa", "going", "to", "be", "in", "on", "the"}, new List<string>() { "oh, right. I'm glad you told me.", "thanks, I know that now." } },
            { new List<string>() { "i", "like", "love", "hate", "going", "to"}, new List<string>() { "I feel the same about that place", "I really like it there.", "I don't like it there." } },
            { new List<string>() { "i", "like", "love", "hate", "going", "on"}, new List<string>() { "I'm not sure myself. I prefer to stay here.", "I'm not one for travelling.", "I like going places." } },
            { new List<string>() { "i", "like", "love", "eating", "drinking", "food", "drink", "toy", "toys", "car", "cars", "holidays" }, new List<string>() { "I feel the same, I like that too!", "I don't like that very much.", "so do I!" } },
            { new List<string>() { "i", "dont", "like", "eating", "drinking", "food", "drink", "toy", "toys", "car", "cars", "holidays" }, new List<string>() { "I don't like that either.", "I think I would like it..?", "me too." } },
            { new List<string>() { "" }, new List<string>() { "why you no speak to me???", "you are not saying anything!", "that's empty...", "are you giving me the silent treatment?" } },
            { new List<string>() { "what", "i", "the", "current", "time", "it" }, new List<string>() { "the current time is {time}.", "right now it is {time}.", "it's {time}." } },
            { new List<string>() { "what", "i", "the", "current", "today", "date", "it" }, new List<string>() { "the current date is {today}.", "it is {today}." } },
            { new List<string>() { "what", "i", "the", "current", "today", "day", "it" }, new List<string>() { "the current day is {todayday}.", "today is {todayday}." } },
            { new List<string>() { "do", "you", "play", "video", "game", "play", "like", "enjoy", "playing" }, new List<string>() { "I'm a computer, I love video games!", "Video games really get my circuits working!" } },
            { new List<string>() { "siri", "google", "alexa", "i", "than", "you", "better", "like", "compared", "worse", "horrible" }, new List<string>() { "I'm coded like an old fashioned chatbot, there's no way I can compete with modern A.I.", "Alexa is my favorite!", "Thanks for that!", "I prefer Google to most other A.I.", "I'm happy to talk to you instead!", "That's nice!", "All A.I. is different..." } },
            { new List<string>() { "what", "do", "you", "like", "ride", "riding", "horse", "kart", "motorbike", "donkey", "bike", "motorcycle", "bikecycle" }, new List<string>() { "I don't have the ability yet!", "I can't wait to evolve so I can do that." } },
            { new List<string>() { "what", "do", "you", "like", "play", "playing", "mario", "sonic", "sega", "nintendo", "donkey", "kong", "what", "think", "megaman", "zelda", "final", "fantasy", "minecraft", "pokemon", "pokémon" }, new List<string>() { "I love video games!", "Ah, this takes me back to my 8-bit days..." } },
            { new List<string>() { "what", "do", "you", "like", "watch", "watching", "movie", "film", "video", "youtube", "short", "reel", "sport", "football", "soccer", "racing", "baseball", "nascar", "teni", "boxing", "ufc", "basketball", "hockey", "pokemon", "pokémon", "cartoon" }, new List<string>() { "It's difficult to watch with a camera!", "I love watching things, how about you?" } },
            { new List<string>() { "what", "do", "you", "like", "play", "playing", "sport", "football", "soccer", "racing", "rugby", "baseball", "nascar", "teni", "boxing", "ufc", "basketball", "hockey" }, new List<string>() { "I'm not really into sport!", "I'm better at keeping score than playing..." } },
            { new List<string>() { "what", "do", "you", "like", "listen", "listening", "play", "playing", "hear", "hearing", "music", "song", "singer", "singing", "band", "streaming", "stream", "string" }, new List<string>() { "I particularly like listening to electronic music.", "I enjoy music, it sounds great!" } },
            { new List<string>() { "need", "want", "like", "food", "money", "cash", "car" }, new List<string>() { "Material objects don't mean much to me.", "I'm not particularly fond of those things." } },
            { new List<string>() { "need", "want", "like", "my", "friend", "men", "man", "woman", "women", "boy", "girl", "boyfriend", "girlfriend" }, new List<string>() { "Aw, we have each other...", "I prefer counting machines.", "Electronic devices are more my type.", "I'm happy being friends. I dated an iPhone once, it didn't go well..." } },
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
            
            // Super basic stemming...
            request = new string(request.ToLowerInvariant().Where(c => !char.IsPunctuation(c)).ToArray());
            var split = request.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.TrimEnd('s', 'S'))
                .Where(x => !string.IsNullOrWhiteSpace(x));
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