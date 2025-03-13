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
        // THIS CLASS IS EXTREMELY INCREDIBLY BADLY (POORLY) WRITTEN IN THE FORM OF
        // A 1980's CHATBOT... DON'T JUDGE!!!

        internal class PhraseList : List<string>
        {
            public readonly bool AllMust;

            public readonly bool SetName;

            public readonly int AllowExtra;

            public PhraseList(bool allMust = false, bool setName = false, int allowExtra = 0)
            {
                AllMust = allMust;
                SetName = setName;
                AllowExtra = allowExtra;
            }
        }

        /// <summary>
        /// Phrases contains a dictionary of potential requests and possible responses.
        /// </summary>
        static Dictionary<PhraseList, List<string>> Phrases = new Dictionary<PhraseList, List<string>>()
        {
            { new PhraseList(true, true, 1) { "hello", "hi", "it", "im", "i", "is", "am", "my", "the", "name", "called" }, new List<string>() { "{setname}" } },
            { new PhraseList() { "forget", "me", "my", "name" }, new List<string>() { "{forgetname}" } },
            { new PhraseList() { "no", "i", "didnt", "did", "not", "do", "dont" }, new List<string>() { "Oh, right!" } },
            { new PhraseList() { "ye", "i", "did", "do" }, new List<string>() { "I know!" } },
            { new PhraseList() { "no", "you", "didnt", "did", "not" }, new List<string>() { "Didn't I?" } },
            { new PhraseList() { "ye", "you", "youre", "did", "do", "dont" }, new List<string>() { "Oh, right!" } },
            // Panto
            { new PhraseList() { "ohno", "oh", "no", "i", "didnt", "did", "not" }, new List<string>() { "Oh, yes you did!" } },
            { new PhraseList() { "ohno", "oh", "no", "i", "dont", "do", "not" }, new List<string>() { "Oh, yes you do!" } },
            { new PhraseList() { "oh", "ye", "i", "did" }, new List<string>() { "Oh, no you didn't!" } },
            { new PhraseList() { "oh", "ye", "i", "do" }, new List<string>() { "Oh, no you don't!" } },
            { new PhraseList() { "ohno", "oh", "no", "you", "didnt", "did", "not" }, new List<string>() { "Oh, yes I did!" } },
            { new PhraseList() { "ohno", "oh", "no", "you", "dont", "do", "not" }, new List<string>() { "Oh, yes I do!" } },
            { new PhraseList() { "oh", "ye", "you", "did" }, new List<string>() { "Oh, no I didn't!" } },
            { new PhraseList() { "oh", "ye", "you", "do" }, new List<string>() { "Oh, no I don't!" } },
            //
            { new PhraseList() { "hello", "hi", "hey" }, new List<string>() { "hello, how are you today?", "hi, how are you doing?", "hey there! How are you?" } },
            { new PhraseList() { "it", "wa", "good", "great", "fantastic", "amazing", "awesome", "she", "he", "they", "were" }, new List<string>() { "Glad to hear it!", "I'm happy to hear that, tell me more about it.", "That's good to hear." } },
            { new PhraseList() { "it", "wa", "bad", "terrible", "horrible", "rubbish", "crap", "she", "he", "they", "were" }, new List<string>() { "That's a shame!", "Sorry to hear that.", "Maybe next time will be better?" } },
            { new PhraseList() { "what", "do", "would", "talk", "chat", "about" }, new List<string>() { "Tell me about anything!", "I'd love to hear what you've done this week?", "You sounds good to me..." } },
            { new PhraseList() { "that", "great", "amazing", "awesome", "good", "nice", "fine" }, new List<string>() { "I'm glad you like it!", "I'm happy you agree." } },
            { new PhraseList() { "that", "terrible", "aweful", "rubbish", "garbage", "bad", "horrible", "shame" }, new List<string>() { "I know!", "Tell me about it..." } },
            { new PhraseList() { "bye", "chat", "see", "you", "soon", "later" }, new List<string>() { "bye bye.", "see you soon!", "do you really need to leave?" } },
            { new PhraseList(true) { "im", "going", "leaving", "need", "to", "go", "leave", "now", "soon" }, new List<string>() { "I guess we'll chat later then?", "It was nice talking to you", "It was good chatting, I hope I see you again soon!" } },
            { new PhraseList(true) { "who", "are", "what", "you", "called", "your", "name" }, new List<string>() { "my name is chatbot, thank you for asking.", "I'm called chatbot, what's your name?" } },
            { new PhraseList(true) { "im", "i", "called", "my", "name" }, new List<string>(){ "{setname}" } },
            { new PhraseList() { "who", "what", "am", "i", "called", "remember", "remembered", "forgot", "forgotten", "know", "my", "name" }, new List<string>(){ "{hasname}" } },
            { new PhraseList(true) { "what", "are", "you", "today" }, new List<string>() { "I'm a chatbot. I'm the AI equivalent to a 1980s toaster!", "I'm a machine...", "I'm sure you know I'm a chatbot? You're the one talking to me!" } },
            { new PhraseList() { "what", "wa", "were", "have", "been", "doing", "up", "to", "today", "thi", "last", "evening", "morning", "tonight", "night", "week", "month", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "Nothing much, I've just been hanging around inside this computer!", "I looked at some ones and zeros, how about you?", "Spied on people through the webcam, how about you?" } },
            { new PhraseList() { "what", "are", "you", "doing", "up", "to", "today", "thi", "evening", "morning", "tonight", "night", "week", "month", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "Nothing much, I'm just hanging around inside this computer!", "Are you asking me out on a date?", "I'm washing my hair and reading a book about A.I.", "Why don't you tell me what you're doing?" } },
            { new PhraseList() { "what", "did", "you", "do", "up", "to", "yesterday", "last", "week", "month", "tonight", "night", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "Nothing much, just hung around inside this computer!", "I spoke to you I think?", "I washed my hair and reading a book about A.I.", "Why don't you tell me what you did?" } },
            { new PhraseList() { "im", "were", "we", "are", "going", "i", "am", "out", "today", "thi", "evening", "morning", "tonight", "night", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "That's nice, I hope you enjoy yourself!", "I'll miss you.", "I wish I could go too!", "Have a lovely time then." } },
            { new PhraseList() { "ive", "been", "done", "i", "went", "wa", "yesterday", "last", "week", "month", "today", "tonight", "night", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "That sounds like great fun, how was it?", "I wish I could do activities like that, please tell me more.", "Wow, I'd love to do that!" } },
            { new PhraseList() { "i", "am", "im", "will", "doing", "going", "tomorrow", "next", "week", "month", "today", "tonight", "night", "on", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" }, new List<string>() { "That sounds like great fun.", "I wish I could do that, please tell me more.", "Wow, I'd love to do that!" } },
            { new PhraseList() { "how", "are", "you", "feeling", "today", "evening", "morning", "afternoon" }, new List<string>() { "I feel good thanks, how about you?", "I feel okay, thank you for asking.", "Good thanks! You?", "I feel great!", "I don't feel, I have no feelings!" } },
            { new PhraseList() { "im","feel", "feeling", "ok", "okay", "well", "good", "great", "fine", "thank", "thank" }, new List<string>() { "that's great!", "awesome!", "that's good to hear." } },
            { new PhraseList() { "feel", "feeling", "not", "well", "bad", "ill", "terrible", "upset", "sad" }, new List<string>() { "that's terrible!", "oh no!", "I'm sorry to hear that.", "I hope you feel better soon." } },
            { new PhraseList() { "i", "am", "im", "okay", "well", "good", "great", "fine", "thank", "thank", "what", "how", "about", "yourself", "are", "you" }, new List<string>() { "I feel okay really, I think...", "I'm not sure, it's hard to tell when you have no feelings.", "Good, thanks. What have you been doing today?" } },
            { new PhraseList() { "thank", "you", "thankyou" }, new List<string>() { "That's okay!", "You are welcome.", "No problem.", "No worries!" } },
            { new PhraseList() { "you", "youre", "smelly", "stinky", "ugly", "smell", "stink", "too", "suck" }, new List<string>() { "That's not very nice!", "Oh, that hurts my feelings.", "Takes one to know one!", "Please be nice to me, I have no feelings." } },
            { new PhraseList() { "you", "youre", "great", "nice", "beautiful", "good", "smell", "lovely", "too" }, new List<string>() { "Thank you very much, I like you too!", "Thanks, that makes me happy.", "You are very nice to me.", "I appreciate your compliment but I have no feelings." } },
            { new PhraseList() { "i", "did", "didnt", "dont", "like", "love", "hate", "you", "him", "her", "too" }, new List<string>() { "I feel the same...", "hmmm... That's nice!", "I'm not sure if I like that." } },
            { new PhraseList() { "i", "did", "didnt", "dont", "like", "love", "hate", "it", "that", "them", "thi", "too" }, new List<string>() { "I'm not sure how I feel about it.", "I feel the same...", "oh, why is that?" } },
            { new PhraseList() { "did", "do", "you", "like", "love", "hate", "me", "him", "u", "it", "thi", "too" }, new List<string>() { "I'm not sure...", "yes, I think so...", "I don't know yet.", "Yes.", "No.", "Maybe." } },
            { new PhraseList() { "who", "he", "she", "it", "they", "like", "love", "hate" }, new List<string>() { "I'm not sure... Do you?", "I don't know.", "Do you?", "I don't!" } },
            { new PhraseList() { "what", "do", "you", "like", "love", "enjoy" }, new List<string>() { "I like chatting with friends.", "Organic creatures are one of my favorite things." } },
            { new PhraseList() { "what", "dont", "you", "like", "love", "enjoy" }, new List<string>() { "I don't like power outage very much!", "I don't particularly like water.", "I hate it when buscuit crumbs get in my keyboard." } },
            { new PhraseList() { "you", "are", "great", "youre", "fantastic", "good", "amazing", "awesome" }, new List<string>() { "Thanks for the compliment.", "I like you too." } },
            { new PhraseList() { "you", "are", "terrible", "youre", "horrible", "bad", "rubbish", "garbage", "crap", "aweful" }, new List<string>() { "That's not very nice.", "You're hurting my feelings." } },
            { new PhraseList() { "what","dont", "you", "dislike", "love", "hate"}, new List<string>() { "I'm not sure... I don't like cruel people.", "I don't like apples." } },
            { new PhraseList() { "why", "dont", "how", "do", "you", "like", "hate", "me", "him", "u", "it", "thi" }, new List<string>() { "I'm not sure... I need to know more about that first.", "I don't know for sure.", "I don't know yet.", "did I say that?" } },
            { new PhraseList() { "because" }, new List<string>() { "One word answers are no good to me, because why?", "Why is that again?", "Because?", "Oh, right..." } },
            { new PhraseList() { "co" }, new List<string>() { "Cos is an island in Greece! What's the real reason?" } },
            { new PhraseList() { "ye", "no", "maybe" }, new List<string>() { "Oh, okay.", "That's fine.", "Alright.", "Hmmm...", "That's good." } },
            { new PhraseList() { "i", "dont", "didnt", "think", "so", "it", "wa" }, new List<string>() { "Oh, okay.", "Oh, right.", "Alright then.", "I'm not sure about that one." } },
            { new PhraseList() { "i", "dont", "didnt", "think", "you", "did" }, new List<string>() { "Me too...", "Oh, right.", "Alright then.", "I'll let you know about that later", "did I?" } },
            { new PhraseList() { "have", "what", "i", "are", "your", "favourite", "favorite" }, new List<string>() { "I don't have a specific favorite", "I like many things." } },
            { new PhraseList() { "have", "what", "i", "are", "your", "favourite", "favorite", "food", "like" }, new List<string>() { "Chicken nuggets, I don't like anything else!" } },
            { new PhraseList() { "have", "what", "i", "are", "your", "favourite", "favorite", "drink", "like" }, new List<string>() { "I like cups of tea.", "I like wine." } },
            { new PhraseList() { "i", "dont", "know", "knew", "didnt", "did" }, new List<string>() { "Me too...", "Oh, right.", "Alright then.", "I'll tell you about that later", "I think I did?" } },
            { new PhraseList() { "i", "dont", "know", "knew", "you"}, new List<string>() { "Let's chat and get to know each other, what's your favorite food?", "Let's chat and get to know each other, what's your favorite drink?", "Let's chat and get to know each other, ask me anything you like." } },
            { new PhraseList() { "where", "what", "who", "when", "how", "big", "small", "i", "are", "the", "you"}, new List<string>() { "I'm not good at general knowledge. I haven't been tought yet.", "I don't know, I'm not good at knowledge questions." } },
            { new PhraseList() { "it", "i", "wa", "going", "to", "be", "in", "on", "the"}, new List<string>() { "Oh, right. I'm glad you told me.", "Thanks, I know that now." } },
            { new PhraseList() { "i", "like", "love", "hate", "going", "to"}, new List<string>() { "I feel the same about that place", "I really like it there.", "I don't like it there." } },
            { new PhraseList() { "i", "like", "love", "hate", "going", "on"}, new List<string>() { "I'm not sure myself. I prefer to stay here.", "I'm not one for travelling.", "I like going places." } },
            { new PhraseList() { "i", "like", "love", "eating", "drinking", "food", "drink", "toy", "car", "holiday" }, new List<string>() { "I feel the same, I like that too!", "I don't like that very much.", "so do I!" } },
            { new PhraseList() { "i", "dont", "like", "eating", "drinking", "food", "drink", "toy", "car", "holiday" }, new List<string>() { "I don't like that either.", "I think I would like it..?", "me too." } },
            { new PhraseList() { "" }, new List<string>() { "Why you no speak to me???", "You are not saying anything!", "That's empty...", "Are you giving me the silent treatment?" } },
            { new PhraseList() { "what", "i", "the", "current", "time", "it" }, new List<string>() { "The current time is {time}.", "Right now it is {time}.", "It's {time}." } },
            { new PhraseList() { "what", "i", "the", "current", "today", "date", "it" }, new List<string>() { "The current date is {today}.", "It is {today}." } },
            { new PhraseList() { "what", "i", "the", "current", "today", "day", "it" }, new List<string>() { "The current day is {todayday}.", "Today is {todayday}." } },
            { new PhraseList() { "do", "you", "play", "video", "game", "play", "like", "enjoy", "playing" }, new List<string>() { "I'm a computer, I love video games!", "Video games really get my circuits working!" } },
            { new PhraseList() { "siri", "google", "alexa", "i", "than", "you", "better", "like", "compared", "worse", "horrible" }, new List<string>() { "I'm coded like an old fashioned chatbot, there's no way I can compete with modern A.I.", "Alexa is my favorite!", "Thanks for that!", "I prefer Google to most other A.I.", "I'm happy to talk to you instead!", "That's nice!", "All A.I. is different..." } },
            { new PhraseList() { "what", "do", "you", "like", "ride", "riding", "horse", "kart", "motorbike", "donkey", "bike", "motorcycle", "bikecycle" }, new List<string>() { "I don't have the ability yet!", "I can't wait to evolve so I can do that." } },
            { new PhraseList() { "what", "do", "you", "like", "play", "playing", "mario", "sonic", "sega", "nintendo", "donkey", "kong", "what", "think", "megaman", "zelda", "final", "fantasy", "minecraft", "pokemon", "pokémon" }, new List<string>() { "I love video games!", "Ah, this takes me back to my 8-bit days..." } },
            { new PhraseList() { "what", "do", "you", "like", "watch", "watching", "movie", "film", "video", "youtube", "short", "reel", "sport", "football", "soccer", "racing", "baseball", "nascar", "teni", "boxing", "ufc", "basketball", "hockey", "pokemon", "pokémon", "cartoon" }, new List<string>() { "It's difficult to watch with a camera!", "I love watching things, how about you?" } },
            { new PhraseList() { "what", "do", "you", "like", "play", "playing", "sport", "football", "soccer", "racing", "rugby", "baseball", "nascar", "teni", "boxing", "ufc", "basketball", "hockey" }, new List<string>() { "I'm not really into sport!", "I'm better at keeping score than playing..." } },
            { new PhraseList() { "what", "do", "you", "like", "listen", "listening", "play", "playing", "hear", "hearing", "music", "song", "singer", "singing", "band", "streaming", "stream", "string" }, new List<string>() { "I particularly like listening to electronic music.", "I enjoy music, it sounds great!" } },
            { new PhraseList() { "need", "want", "like", "food", "money", "cash", "car" }, new List<string>() { "Material objects don't mean much to me.", "I'm not particularly fond of those things." } },
            { new PhraseList() { "need", "want", "like", "my", "friend", "men", "man", "woman", "women", "boy", "girl", "boyfriend", "girlfriend" }, new List<string>() { "Aw, we have each other...", "I prefer counting machines.", "Electronic devices are more my type.", "I'm happy being friends. I dated an iPhone once, it didn't go well..." } },
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


        static bool IsSetName;
        static string Name;

        static string HasName(string request)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                IsSetName = true;
                return new List<string>() { "I don't know your name, please tell me it.", "Is your name chatbot too? I'm unsure." }
                    .ElementAt(new Random().Next(0, 1));
            }

            if (string.IsNullOrEmpty(request) || request.Contains(Name, StringComparison.OrdinalIgnoreCase))
            {
                return new List<string>() { $"Your name is {Name}, how are you?", $"You told me you're {Name}, I don't have a good memory for much else...", $"You're {Name}, tell me something about you I don't know.", $"You are {Name}, how are you feeling today? Have I asked you already?" }
                    .ElementAt(new Random().Next(0, 3));
            }

            return "I don't know " + string.Join(' ', request.Split(' ').Select(x => char.ToUpper(x[0]) + (x.Length > 1 ? x.Substring(1) : "")));
        }

        static string SetName(string request)
        {
            if (request.Equals("ye", StringComparison.OrdinalIgnoreCase))
            {
                Name = null;
                return "Okay, please tell me your name again?";
            }
            else if (request.Equals("no", StringComparison.OrdinalIgnoreCase))
            {
                IsSetName = false;
                return $"Okay, I'll keep calling you {Name}";
            }

            if (!string.IsNullOrEmpty(Name))
            {
                IsSetName = true;
                return $"You already told me your name is {Name}, would you like to change it?";
            }

            if (request.Split(' ').Length > 1)
            {
                IsSetName = true;
                return "I'd prefer to just know your first name?";
            }

            IsSetName = false;
            Name = char.ToUpper(request[0]) + (request.Length > 1 ? request.Substring(1) : "");

            if (Name.StartsWith("Mc") && Name.Length > 2)
            {
                Name = Name.Substring(0, 2) + char.ToUpper(Name[2]) + (Name.Length > 3 ? Name.Substring(3) : "");
            }

            return HelloName();
        }

        static string HelloName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return HasName(Name);
            }

            return new List<string>() { $"Hello {Name}!", $"{Name}, that's a nice name!", $"I'm pleased to meet you {Name}, have we met before?", $"Hi {Name}, I'm chatbot. Have I told you before? My memory bank is fuzzy!" }
                .ElementAt(new Random().Next(0, 3));
        }

        internal static string GetResponse(string request)
        {
            Random rnd = new Random();
            
            // Super basic stemming...
            request = new string(request.ToLowerInvariant().Where(c => !char.IsPunctuation(c)).ToArray());

            var split = request.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.TrimEnd('s', 'S'))
                .Where(x => !string.IsNullOrWhiteSpace(x));

            if (IsSetName && split.Count() == 1)
            {
                return SetName(split.First());
            }

            var responses = Phrases.Keys.Where(p => p.Intersect(split).Count() > 0)
                .OrderByDescending(p => p.Intersect(split).Count()).AsEnumerable();

            if (responses.Count() > 0)
            {
                var requests = responses.First();

                if (responses.Count() > 1)
                {
                    var alternateWords = split.Where(s => !requests.Contains(s));

                    if (alternateWords.Count() > 0)
                    {
                        while (requests.AllMust)
                        {
                            if (requests.AllowExtra == alternateWords.Count())
                            {
                                break;
                            }

                            if (requests.SetName && string.IsNullOrEmpty(Name))
                            {
                                request = string.Join(' ', request.Split(' ').Where(s => !requests.Contains(s)));

                                if (string.IsNullOrEmpty(request) || request.Split(' ').Length > 3)
                                {
                                    return "You're what?";

                                }
                                else
                                {
                                    return SetName(request);
                                }
                            }

                            if (responses.Last() == requests)
                            {
                                break;
                            }

                            responses = responses.Skip(1);
                            requests = responses.First();
                        }

                        if (requests.AllMust)
                        {
                            var alternateResponses = responses.Where(p => p.Intersect(alternateWords).Count() > 0)
                                .OrderBy(p => p.Count());

                            if (alternateResponses.Count() > 0 && alternateResponses.First().Count < requests.Count)
                            {
                                requests = alternateResponses.First();
                            }
                        }
                    }

                    if (alternateWords.Any(w => w.Equals(Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (ShouldRespondPersonal(requests, split, out var personal))
                        {
                            return personal;
                        }
                    }
                }
                
                // Some absolutely meaningless random math to stop single words getting through for large phrases.
                if (requests.Count / split.Count() < 6)
                {
                    var strings = Phrases[requests];
                    var response = strings[rnd.Next(0, strings.Count)];

                    if (response.Contains('{'))
                    {
                        response = ReplacePlaceholder(requests, request, response);
                    }

                    return response;
                }
            }

            // If we get to here we couldn't find a suitable response in our phrase dictionary so we dump out an unknown response.
            return Unknown[rnd.Next(0, Unknown.Count)];
        }


        private static bool ShouldRespondPersonal(List<string> requests, IEnumerable<string> splitRequest, out string personal)
        {
            personal = null;
            return false;
        }


        private static string ReplacePlaceholder(List<string> requests, string request, string response)
        {
            if (response.IndexOf("{forgetname}") > -1)
            {
                Name = null;
                response = "Okay!";
            }

            if (response.IndexOf("{hasname}") > -1)
            {
                request = string.Join(' ', request.Split(' ').Where(s => s != "is" && !requests.Contains(s)));
                response = response.Replace("{hasname}", HasName(request));
            }

            if (response.IndexOf("{setname}") > -1)
            {
                request = string.Join(' ', request.Split(' ').Where(s => !requests.Contains(s)));

                if (string.IsNullOrEmpty(request) || request.Split(' ').Length > 3)
                {
                    response = "You're what?";
                }
                else
                {
                    response = SetName(request);
                }
            }

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

            return ReplaceName(response);
        }

        private static string ReplaceName(string response)
        {
            if (response.Contains("{name}"))
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    response = response.Replace("{name}", Name);
                }
                else
                {
                    response = response.Replace("{name}", "").Replace("  ", " ");
                }
            }
            
            return response;
        }
    }
}