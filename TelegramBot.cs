
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using WebApplication2.Client;
using Telegram.Bot.Args;

using System.Data;
using MvcMovie.Models;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using WebApplication2.Models;

namespace WebApplication2
{
    
    public class TelegramBot
    {
        TelegramBotClient botClient = new TelegramBotClient("6244466837:AAHKqHhXCngtjKZ-uvIr0_4_LZBJlFThpoc");
        CancellationToken cancellationToken = new CancellationToken();
        ReceiverOptions ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };

        private Dictionary<long, UserParams> userParams = new Dictionary<long, UserParams>();
        public async Task Start()
        {
            botClient.StartReceiving(HandlerUpdateAsync, HandlerError, ReceiverOptions, cancellationToken);
            var botMe = await botClient.GetMeAsync();
            Console.WriteLine($"Bot {botMe.Username} activated");
            Console.ReadKey();

        }

       
        private Task HandlerError(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"API error code:{apiRequestException.ErrorCode}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                await HandlerMessageAsync(botClient, update.Message);
            }
        }
        
        TranslateClient translateClient = new TranslateClient();
        GptClient GptClient = new GptClient();
        BookingId BookingId = new BookingId();
        Booking Booking = new Booking();
        GeocodingClient geocodingClient = new GeocodingClient();
        HistoricalPlacesClient historicalPlacesClient = new HistoricalPlacesClient();
        RestaurantsAroundClient restaurantsAroundClient = new RestaurantsAroundClient();
        RestaurantContactClient restaurantContactClient = new RestaurantContactClient();
        InvGeocodingClient invGeocodingClient = new InvGeocodingClient();
        WikiClient wikiClient = new WikiClient();
        YTClient yTClient = new YTClient();

        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            if (!userParams.ContainsKey(message.Chat.Id))
            {
                userParams.Add(message.Chat.Id, new UserParams());
            }

            UserParams userParameters = userParams[message.Chat.Id];
            //translateClient.TranslateAsync();


            if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть команду /menu");

            }
            else if (message.Text == "Меню"|| message.Text == "/menu")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                        new[]
                        {
                        new KeyboardButton [] {"Translate", "Ask ChatGPT" },
                        new KeyboardButton [] {"Booking","Улюбленні страви" },
                        new KeyboardButton [] {"Цікаві місця навколо адреси", "Wiki" },
                        new KeyboardButton [] {"Меню" }
                        }
                    )
                {

                    ResizeKeyboard = true

                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choose part of menu", replyMarkup: replyKeyboardMarkup);

            }
            //translating
            else if (message.Text == "Translate")
            {
                userParameters.step = "";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть код мови з якої ви хочете перевести");
                await botClient.SendTextMessageAsync(message.Chat.Id, "Украіїнська - uk\nАнгліїська - en");

                userParameters.step = "Lang to";
            }
            else if (userParameters.step == "Lang to")
            {
                userParameters.step = "";
                userParameters.from = message.Text.ToLower();
                await botClient.SendTextMessageAsync(message.Chat.Id, userParameters.from);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть код мови на яку ви хочете перелести");
                userParameters.step = "Lang text";
            }
            else if (userParameters.step == "Lang text")
            {
                userParameters.step = "";
                userParameters.to = message.Text.ToLower();
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть текст який ви хочете перелести");
                userParameters.step = "result";
            }
            else if (userParameters.step == "result")
            {
                userParameters.text = message.Text;
                try
                {
                    userParameters.step = "";
                    var res = await translateClient.TranslateAsync(userParameters.text, userParameters.from, userParameters.to);
                    await botClient.SendTextMessageAsync(message.Chat.Id, res.data.translatedText);
                }
                catch (Exception e)
                {

                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу данних");
                }



                userParameters.step = ""; userParameters.to = ""; userParameters.from = ""; userParameters.text = "";
            }
            //Asking chat gpt
            else if (message.Text == "Ask ChatGPT")
            {
                userParameters.step = "";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть ваш запит");
                userParameters.step = "GPT input";
            }
            else if (userParameters.step == "GPT input")
            {
                userParameters.text = message.Text;
                try
                {

                    var res1 = await GptClient.AskGPTAsync(userParameters.text);
                    await botClient.SendTextMessageAsync(message.Chat.Id, res1.choices[0].message.content);
                }
                catch { await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу данних"); }

                userParameters.step = "";

            }
            //bookingId
            else if (message.Text == "Booking")
            {
                userParameters.step = "";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть місто або регіон, одним словом, англійською");
                userParameters.step = "Booking Place input";
            }
            else if (userParameters.step == "Booking Place input")
            {
                userParameters.text = message.Text;

                try
                {
                    userParameters.step = "";
                    var res2 = await BookingId.BookingIDAsync(userParameters.text);
                    await botClient.SendTextMessageAsync(message.Chat.Id, ("Введіть дату заїзду в готель, формат: yyyy-mm-dd"));
                    userParameters.DestType = res2[0].DestinationType;
                    userParameters.DestId = res2[0].DestId;
                    userParameters.step = "CheckInDate"; userParameters.text = "";
                }
                catch
                {
                    userParameters.step = "";
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу данних");
                }

            }
            //Booking
            else if (userParameters.step == "CheckInDate")
            {
                userParameters.step = "";
                userParameters.CheckInDate = message.Text;
                await botClient.SendTextMessageAsync(message.Chat.Id, ("Введіть дату виїзду з готеля, формат: yyyy-mm-dd"));
                userParameters.step = "CheckOutDate";
            }
            else if (userParameters.step == "CheckOutDate")
            {
                userParameters.step = "";
                userParameters.CheckOutDate = message.Text;
                await botClient.SendTextMessageAsync(message.Chat.Id, ("Введіть кількість дорослих, формат: число"));
                userParameters.step = "NumOfAdult";
            }
            else if (userParameters.step == "NumOfAdult")
            {
                userParameters.step = "";
                userParameters.NumOfAdults = message.Text;
                await botClient.SendTextMessageAsync(message.Chat.Id, ("Введіть кількість бажаних кімнат, формат: число"));
                userParameters.step = "NumOfRooms";
            }
            else if (userParameters.step == "NumOfRooms")
            {
                userParameters.step = "";
                userParameters.NumOfRooms = message.Text;
                await botClient.SendTextMessageAsync(message.Chat.Id, ("Введіть кількість дітей, формат: число"));
                userParameters.step = "NumOfChildren";
            }
            else if (userParameters.step == "NumOfChildren")
            {
                userParameters.step = "";
                userParameters.NumOfChildren = message.Text;
                await botClient.SendTextMessageAsync(message.Chat.Id, ("Введіть вік дітей, наприклад: 12,13,9 "));
                userParameters.step = "AgeOfChildren";
            }
            else if (userParameters.step == "AgeOfChildren")
            {
                userParameters.AgeOfChildren = message.Text;
                try
                {
                    userParameters.step = "";
                    var res3 = await Booking.BookingHotelAsync(userParameters.CheckInDate, userParameters.DestType, userParameters.CheckOutDate, userParameters.NumOfAdults, userParameters.DestId, userParameters.NumOfRooms, userParameters.NumOfChildren, userParameters.AgeOfChildren);
                    if (res3 == null)
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, ("Немає варіантів"));
                    }
                    else if (res3.Hotels.Count < 15)
                    {
                        foreach (var item in res3.Hotels)
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{item.Name}\nМінімальна ціна: {item.MinTotalPrice}\nПосилання на готель: {item.Url}\nАдреса: {item.Address}");
                        }
                    }
                    else if (res3.Hotels.Count > 15)
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{res3.Hotels[i].Name}\nМінімальна ціна: {res3.Hotels[i].MinTotalPrice}\nПосилання на готель: {res3.Hotels[i].Url}\nАдреса: {res3.Hotels[i].Address}");
                            userParameters.step = "";
                            userParameters.DestType = "";
                            userParameters.DestId = "";
                            userParameters.CheckInDate = "";
                            userParameters.CheckOutDate = "";
                            userParameters.NumOfAdults = "";
                            userParameters.NumOfRooms = "";
                            userParameters.NumOfChildren = "";
                            userParameters.AgeOfChildren = "";
                        }
                    }
                }
                catch { await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу данних"); userParameters.step = ""; }

            }
            //ext buttons
            else if (message.Text == "Цікаві місця навколо адреси")
            {
                userParameters.step = "";
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                        new[]
                        {
                        new KeyboardButton [] {"Історичні місця", "Кафе, ресторани, бари і тд" },
                        new KeyboardButton [] {"Меню"},

                        }
                    )
                {

                    ResizeKeyboard = true

                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choose part of menu", replyMarkup: replyKeyboardMarkup);
                return;
            }
            else if (message.Text == "Цікаві місця навколо мене")
            {
                userParameters.step = "";
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                        new[]
                        {
                        new KeyboardButton [] {"Історичні місця навколо", "Ресторани навколо" },
                        new KeyboardButton [] {"Меню"},

                        }
                    )
                {

                    ResizeKeyboard = true

                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choose part of menu", replyMarkup: replyKeyboardMarkup);
                return;
            }
            //historical places
            //geocoding block
            else if (message.Text == "Історичні місця")
            {
                userParameters.step = "";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть адресу англійською мовою");

                userParameters.step = "Input address for geocoding";
            }
            else if (userParameters.step == "Input address for geocoding")
            {
                userParameters.step = "";
                
                userParameters.text = message.Text;
                try
                {
                    userParameters.lattitude = "";
                    userParameters.longtitude = "";
                    var res4 = await geocodingClient.GeocodingAsync(userParameters.text);
                 userParameters.lat = Convert.ToString(res4.results[0].geometry.location.lat);
                    for (int c = 0; c < userParameters.lat.Length; c++)
                    {
                        if (userParameters.lat[c] == ',')
                        {
                            userParameters.lattitude = userParameters.lattitude + ".";
                        }
                        else
                        {
                            userParameters.lattitude = userParameters.lattitude + userParameters.lat[c];
                        }

                    }
                    
                    userParameters.lon = Convert.ToString(res4.results[0].geometry.location.lng);
                    for (int c = 0; c < userParameters.lon.Length; c++)
                    {
                        if (userParameters.lon[c] == ',')
                        {
                            userParameters.longtitude = userParameters.longtitude + ".";
                        }
                        else
                        {
                            userParameters.longtitude = userParameters.longtitude + userParameters.lon[c];
                        }

                    }
                    
                    
                    if (userParameters.lattitude.Length > 9)
                    {
                        userParameters.lattitude = userParameters.lattitude.Substring(0, 9);
                    }

                    if (userParameters.longtitude.Length > 9)
                    {
                        userParameters.longtitude = userParameters.longtitude.Substring(0, 9);
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, userParameters.lattitude);
                    await botClient.SendTextMessageAsync(message.Chat.Id, userParameters.longtitude);
                    userParameters.step = "Input radius";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть радіус (метри)");
                    userParameters.text = "";
                }
                catch { await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу данних"); }
            }

            /*
            //historical places
            else if (userParameters.step == "Input radius")
            {
                userParameters.step = "";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть кількість місць");
               
                userParameters.step = "Input NumOfPlaces";
            }
            */
            else if (userParameters.step == "Input radius")
            {
                userParameters.step = "";
                userParameters.text = message.Text;
                try
                {

                    
                    var res5 = await historicalPlacesClient.HistoricalPlacesAroundAsync(userParameters.lattitude, userParameters.longtitude, userParameters.text, 3);
                    
                        for (int i = 0; i < 3; i++)
                        {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Завантаження"); 
                        string link = "";
                            userParameters.from = "auto";
                            userParameters.to = "en";
                            try { var chat = await GptClient.AskGPTAsync($"Роскажи українською мовою про{res5.features[i].properties.name}, викорситай 20-30 слів"); 
                            userParameters.text = chat.choices[0].message.content; } catch { userParameters.text = "Немає інформації"; }
                            
                            
                            var restrans = await translateClient.TranslateAsync(res5.features[i].properties.name, userParameters.from, userParameters.to);
                            try
                            {
                                var resYT = await yTClient.YTAsync(res5.features[i].properties.name);
                                link = resYT.items[0].url;
                            }
                            catch { link = ""; }




                            
                            if (res5.features[i].properties.name != "") { await botClient.SendTextMessageAsync(message.Chat.Id, ($"{i+1}. {res5.features[i].properties.name}\n{userParameters.text}\n{link}")); }





                        }
                    
                    
                }
                catch { await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу данних або більше немає результатів"); userParameters.step = ""; }


                userParameters.step = "";

            }
            //cafe
            else if (message.Text == "Кафе, ресторани, бари і тд")
            {
                userParameters.step = "";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть адресу англійською мовою");

                userParameters.step = "Input address for geocoding2";
            }
            else if (userParameters.step == "Input address for geocoding2")
            {
                userParameters.step = "";
                userParameters.lattitude = "";
                userParameters.longtitude = "";
                userParameters.text = message.Text;
                try
                {
                    var res4 = await geocodingClient.GeocodingAsync(userParameters.text);
                    userParameters.lat = Convert.ToString(res4.results[0].geometry.location.lat);
                    for (int c = 0; c <userParameters.lat.Length; c++)
                    {
                        if (userParameters.lat[c] == ',')
                        {
                            userParameters.lattitude = userParameters.lattitude + ".";
                        }
                        else
                        {
                            userParameters.lattitude = userParameters.lattitude + userParameters.lat[c];
                        }

                    }
                    
                    userParameters.lon = Convert.ToString(res4.results[0].geometry.location.lng);
                    for (int c = 0; c < userParameters.lon.Length; c++)
                    {
                        if (userParameters.lon[c] == ',')
                        {
                            userParameters.longtitude = userParameters.longtitude + ".";
                        }
                        else
                        {
                            userParameters.longtitude = userParameters.longtitude + userParameters.lon[c];
                        }

                    }

                    if (userParameters.lattitude.Length > 9)
                    {
                        userParameters.lattitude = userParameters.lattitude.Substring(0, 9);
                    }

                    if (userParameters.longtitude.Length > 9)
                    {
                        userParameters.longtitude = userParameters.longtitude.Substring(0, 9);
                    }
                    await botClient.SendTextMessageAsync(message.Chat.Id, userParameters.lattitude);
                    await botClient.SendTextMessageAsync(message.Chat.Id, userParameters.longtitude);
                    userParameters.step = "Input radius2";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть радіус (метри)");
                }
                catch { await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу данних"); }

                userParameters.text = "";
            }
            
            else if (userParameters.step == "Input radius2")
            {
                userParameters.step = "";
                userParameters.text = message.Text;
                userParameters.step = "Input kind";
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                        new[]
                        {
                        new KeyboardButton [] {"Restaurant", "Bar" },                        
                        new KeyboardButton [] {"Меню" }
                        }
                    )
                {

                    ResizeKeyboard = true

                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Оберіть тип закладу", replyMarkup: replyKeyboardMarkup);
            }
            else if (userParameters.step == "Input kind")
            {
                userParameters.step = "";
                userParameters.kind = message.Text.ToLower();
                userParameters.step = "Input keyword";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Оберіть ключове слово\nПриклади: можна використовувати назви кухонь, irish, british, italian, asian\nТакож можна ввести назву страви, наприклад - pizza\nНазви тільки англійською, одним словом. Варіанти Bar та Restaurant не будуть працювати");
            }
            
            else if (userParameters.step == "Input keyword")
            {
                userParameters.keyword = message.Text.ToLower();
                userParameters.step = "";
                
                try
                {
                   
                    var res6 = await restaurantsAroundClient.RestaurantsAroundAsync(userParameters.lattitude, userParameters.longtitude, userParameters.text, userParameters.kind, userParameters.keyword);
                    
                        for (int i = 0; i < 5; i++)
                        {
                            userParameters.lattitude = "";
                            userParameters.longtitude = "";
                            userParameters.step = "location";
                            userParameters.longtitude = res6.results[i].geometry.location.lng;
                            userParameters.lattitude = res6.results[i].geometry.location.lat;
                            userParameters.text = res6.results[i].place_id;
                            
                            var res7 = await restaurantContactClient.RestaurantsContactsAsync(userParameters.text);
                            userParameters.opennow = false;
                            userParameters.delivery = false;
                            userParameters.phoneNumber = "";
                            if (res6.results[i].opening_hours != null) { userParameters.opennow = res6.results[i].opening_hours.open_now; }
                            if (res7.result.delivery != null) {userParameters.delivery = res7.result.delivery; }
                            if (res7.result.formatted_phone_number != null) { userParameters.phoneNumber = res7.result.formatted_phone_number; }
                            userParameters.TrueFalse1 = "Ні";
                            userParameters.TrueFalse12 = "Ні";
                            if (userParameters.opennow == true) { userParameters.TrueFalse1 = "Так"; }
                            if (userParameters.delivery == true) {userParameters.TrueFalse12 = "Так"; }
                            await botClient.SendTextMessageAsync(message.Chat.Id, ($"Назва: {res6.results[i].name}\nЦінова категроя: {res6.results[i].price_level}$\nОцінка відвіувачів: {res6.results[i].rating}\nВідкрито зараз: {userParameters.TrueFalse1}\nНаявна доставка: {userParameters.TrueFalse12}\nНомер телефона: {userParameters.phoneNumber}" +
                               $"\nurl: {res7.result.url}"));

                            if (userParameters.step == "location")
                            {
                                userParameters.lat = "";
                                userParameters.lon = "";
                                for (int c = 0; c < userParameters.lattitude.Length; c++)
                                {
                                    if (userParameters.lattitude[c] == '.')
                                    {
                                        userParameters.lat = userParameters.lat + ",";
                                    }
                                    else
                                    {
                                        userParameters.lat = userParameters.lat + userParameters.lattitude[c];
                                    }
                                }
                                for (int c = 0; c <userParameters.longtitude.Length; c++)
                                {
                                    if (userParameters.longtitude[c] == '.')
                                    {
                                        userParameters.lon = userParameters.lon + ",";
                                    }
                                    else
                                    {
                                        userParameters.lon = userParameters.lon + userParameters.longtitude[c];
                                    }
                                }





                                var res10 = await invGeocodingClient.InvGeocodingAsync(userParameters.lattitude, userParameters.longtitude);
                                userParameters.adres = res10.PlusCode.CompoundCode;
                                userParameters.num1 = float.Parse(userParameters.lat);
                                userParameters.num2 = float.Parse(userParameters.lon);

                                userParameters.text = res6.results[i].name;
                                Message point = await botClient.SendVenueAsync(
                                    chatId: message.Chat.Id,
                                    latitude: userParameters.num1,
                                    longitude: userParameters.num2,
                                    title: $"{userParameters.text}",
                                    address: $"{userParameters.adres}",
                                    cancellationToken: cancellationToken);



                            }
                        }
                    
                    
                }
                catch{ await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу данних або не залишилось варіантів"); }

            }
            else if (message.Text == "Улюбленні страви")
            {
                userParameters.step = "";
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                   (
                       new[]
                       {
                        new KeyboardButton [] {"Список", "Видалити" },
                        new KeyboardButton [] {"Додати", "Меню" },


                       }
                   )
                {

                    ResizeKeyboard = true

                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choose part of menu", replyMarkup: replyKeyboardMarkup);
            }
            else if (message.Text == "Список")
            {
                userParameters.step = "";
                DB_GET dB_GET = new DB_GET();
                var res = await dB_GET.GetAsync();
                userParameters.count = 0;
                    for (int i = 0; i < res.Count; i++)
                    {
                        if (message.Chat.Id == res[i].chatID)
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"Номер: {res[i].id}\n{res[i].name}\nОцінка: {res[i].rating}");
                            userParameters.count = userParameters.count+1;
                        }

                    }
                    if (userParameters.count == 0) 
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"Список пустий");

                    }
                userParameters.step = "";
                





            }
            else if (message.Text == "Видалити")
            {
                userParameters.step = "";

                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть номер");
                userParameters.step = "Delete";

            }
            else if (userParameters.step == "Delete")
            {
                userParameters.step = "";
                DB_DELETE dbDelete = new DB_DELETE();
                try
                {
                    userParameters.idToDelete = int.Parse(message.Text);
                    await dbDelete.DeleteAsync(userParameters.idToDelete);
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Страва видалена");
                }
                catch(Exception e) { await botClient.SendTextMessageAsync(message.Chat.Id, $"Елемента з таким номером не існує {e.Message}"); }


                userParameters.step = "";
            }
            else if (message.Text == "Додати")
            {
                userParameters.step = "";
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть назву страви");
                userParameters.step = "Додати2";
            }
            else if (userParameters.step == "Додати2")
            {
                userParameters.step = "";
                userParameters.name = message.Text; ;
                await botClient.SendTextMessageAsync(message.Chat.Id, "Виберіть оцінку");
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                   (
                       new[]
                       {
                        new KeyboardButton [] {"1", "2" },
                        new KeyboardButton [] { "3", "4" },
                        new KeyboardButton [] { "5", "Меню" },


                       }
                   )
                {

                    ResizeKeyboard = true

                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choose part of menu", replyMarkup: replyKeyboardMarkup);
                userParameters.step = "Додати3";
            }
            else if (userParameters.step == "Додати3")
            {
                userParameters.step = "";
                userParameters.rating = int.Parse(message.Text);
                DB_POST dbPost = new DB_POST();
                FavoriteDishPost favoriteDish = new FavoriteDishPost
                {
                    id = 0,
                    chatID = message.Chat.Id,
                    name = userParameters.name,
                    rating = userParameters.rating,
                };
                try { HttpResponseMessage response = await dbPost.PostAsync(favoriteDish); await botClient.SendTextMessageAsync(message.Chat.Id, $"Додано успішно"); } 
                catch 
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Помилка вводу"); 
                }
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                   (
                       new[]
                       {
                        new KeyboardButton [] {"Список", "Видалити" },
                        new KeyboardButton [] {"Додати", "Меню" },

                       }
                   )
                {

                    ResizeKeyboard = true

                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Choose part of menu", replyMarkup: replyKeyboardMarkup);

                userParameters.step = "";
            }
            else if(message.Text == "Wiki") 
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть тему англійською");
                userParameters.step = "WikiSearch";
            }
            else if (userParameters.step == "WikiSearch") 
            {
                WikiClient wikiClient = new WikiClient();
                userParameters.text = message.Text;
                try 
                {
                    var wikires = await wikiClient.WikiAsync(userParameters.text, 3);
                    for(int i = 0; i < 2; i++) 
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{wikires.Summary[i]}");
                    }
                }
                catch { await botClient.SendTextMessageAsync(message.Chat.Id, "Немає результатів"); }
            }
            else { await botClient.SendTextMessageAsync(message.Chat.Id, "Неправильна відповідь"); }
            
        }

    }
}

