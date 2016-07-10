using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.Media;
using System.IO;
using NAudio.Wave;
using NAudio.Lame;

namespace NoobDevBot
{
    class Program
    {
        static TelegramBotClient bot;
        static ReplyKeyboardMarkup markup;
        static SpeechSynthesizer spsy;

        static void Main(string[] args)
        {
            bot = new TelegramBotClient("232609616:AAGOJkDYQmSmUFhjToCt3JTtRnlWp3-TszE");
            markup = new ReplyKeyboardMarkup();
            spsy = new SpeechSynthesizer();

            keyboard();
            bot.OnMessage += bot_OnMessage;

            bot.StartReceiving();





            Console.ReadKey();
        }

        private static async void bot_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine($"{e.Message.From.Username ?? e.Message.From.FirstName}: {e.Message.Text}");
            switch (e.Message.Text)
            {
                case "/nextStream": await bot.SendTextMessageAsync(e.Message.Chat.Id, DateTime.UtcNow.ToLongDateString(), replyMarkup: markup); break;
                case "/sayHello": await bot.SendTextMessageAsync(e.Message.Chat.Id, $"Hallo {e.Message.From.Username ?? e.Message.From.FirstName}", replyMarkup: markup); break;
                case "/speakHello":

                    using (var stream = new MemoryStream())
                    {
                        using (var outStream = new MemoryStream())
                        {

                            string filename = e.Message.From.Username ?? e.Message.From.FirstName + ".mp3";
                            spsy.SetOutputToAudioStream(stream, new SpeechAudioFormatInfo(44100, AudioBitsPerSample.Sixteen, AudioChannel.Mono));

                            spsy.Speak($"Hallo {e.Message.From.Username ?? e.Message.From.FirstName}");
                            stream.Position = 0;
                            int seconds;
                            WaveFormat format = new WaveFormat(44100, 1);
                            using (var mp3file = new LameMP3FileWriter(outStream, format, 128))
                            {
                                stream.CopyTo(mp3file);
                                //((stream.Length) / (sampleRate * (bitRate / 8))) / channels
                                seconds = (int)(stream.Length / (44100 * 2)); //(int)wavefile.CurrentTime.TotalSeconds;
                            }

                            outStream.Position = 0;
                            FileToSend fts = new FileToSend("Hello.mp3", outStream);

                            await bot.SendAudioAsync(e.Message.Chat.Id, fts, seconds, "Cortana", "Hallo");

                        }
                    }
                    break;
                default: await bot.SendTextMessageAsync(e.Message.Chat.Id, "I don't know, what do to with this command. Plase be so kind to use the keyboard, so you don't enter the wrong commands. Thank you very much.", replyMarkup: markup); break; 
            }
            
        }

        private static void keyboard()
        {
            var button00 = new KeyboardButton();
            button00.Text = "/sayHello";
            var button01 = new KeyboardButton();
            button01.Text = "/nextStream";
            var button02 = new KeyboardButton();
            button02.Text = "/speakHello";
            markup.Keyboard = new KeyboardButton[1][];
            markup.Keyboard[0] = new KeyboardButton[3];
            markup.Keyboard[0][0] = button00;
            markup.Keyboard[0][1] = button01;
            markup.Keyboard[0][2] = button02;
        }
    }
}
