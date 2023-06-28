using Newtonsoft.Json;
using WebApplication2.Client;
using WebApplication2.Models;


/*
 //ChatGPT
GptClient gptClient = new GptClient();
string txt = "what is laptop? answer in 20 words";
var res = gptClient.AskGPTAsync(txt);

//Console.WriteLine(res.Result.choices[0].message.content);
*/
/*
 //wiki
WikiClient wikiClient = new WikiClient();
string topic = "chernobyl disaster";
int NumOFarticles = 2;
var res2 = wikiClient.WikiAsync(topic, NumOFarticles);
Console.WriteLine(res2.Result.Title);
Console.WriteLine(res2.Result.Url);
foreach(var item in res2.Result.Summary) 
{
    Console.WriteLine(item);
}
*/
/*
//YouTube
YTClient yTClient = new YTClient();
string topic2 = "beer fest";
var res3 = yTClient.YTAsync(topic2);
for(int i = 0; i < 3; i++)
{
    Console.WriteLine(res3.Result.items[i].url);
}
*/
/*
//translate
TranslateClient translateClient = new TranslateClient();
string text2 = "my name is boris";
string FROM = "EN";
string TO = "uk";
var res4 = translateClient.TranslateAsync(text2, FROM, TO);
Console.WriteLine(res4.Result.TranslatedText);
*/
/*
//Geocoding
GeocodingClient geocodingClient = new GeocodingClient();
string place = "St. Kubanskoy Ukrainy, 47A, Kyiv, Ukraine, 02000";
string en = "en";
var res5 = geocodingClient.GeocodingAsync(place,en);
Console.WriteLine(res5.Result.results[0].geometry.location.lat+"\t"+ res5.Result.results[0].geometry.location.lng);
*/
/*
//invGeocoding
InvGeocodingClient invGeocodingClient = new InvGeocodingClient();
string lat = "50.4859019";
string lon = "30.6290999";
var res6 = invGeocodingClient.InvGeocodingAsync(lat, lon);

Console.WriteLine(res6.Result.Results[1].FormattedAddress);
/*
*/
/*
string lat = "50.4859019";
string lon = "30.6290999";
string radius = "1500";

int NumOfPlaces = 10;

//find historical places near by in radius
HistoricalPlacesClient historicalPlacesClient = new HistoricalPlacesClient();

var res7 = historicalPlacesClient.HistoricalPlacesAroundAsync(lat, lon,radius,NumOfPlaces );

var NearByResponse = res7.Result;

for(int i = 0;i<NumOfPlaces;i++) 
{
    string latitude = NearByResponse.features[i].geometry.coordinates[1];
    string longtitude = NearByResponse.features[i].geometry.coordinates[0];
    Console.WriteLine($"#{i};{NearByResponse.features[i].properties.name}, kind of place: {NearByResponse.features[i].properties.kinds}  ");
    
    
}
*/
/*
//find restaurants around
string lat = "47";
string lon = "19";
string rad = "15000";
string kind = "restaurant";
string keyword = "cafe";
RestaurantsAroundClient restaurantsAroundClient = new RestaurantsAroundClient();
var res8 = restaurantsAroundClient.RestaurantsAroundAsync(lat, lon, rad,kind, keyword);
for(int i = 0; i < 10; i++) 
{
    
    string longtitude = res8.Result.results[i].geometry.location.lng;
    string latitude = res8.Result.results[i].geometry.location.lat;
    string placeid = res8.Result.results[i].place_id;
    Console.WriteLine($"name = {res8.Result.results[i].name}, price rating = {res8.Result.results[i].price_level}, user score from 1 to 5 = {res8.Result.results[i].rating};{res8.Result.results[i].place_id} ");
    if (res8.Result.results[i].opening_hours != null) { Console.WriteLine($"is open now? = {res8.Result.results[i].opening_hours.open_now}"); }
}
*/
/*
//restaurantsContacts
string placeid = "ChIJLXFtk27cQUcR827tB-ozSqo";
RestaurantContactClient restaurantContactClient = new RestaurantContactClient();
var res9 = restaurantContactClient.RestaurantsContactsAsync(placeid);
if (res9.Result.result.delivery  != null) { Console.WriteLine(res9.Result.result.delivery); }
if (res9.Result.result.formatted_phone_number != null) { Console.WriteLine(res9.Result.result.delivery); }
if (res9.Result.result.url != null) { Console.WriteLine(res9.Result.result.url); }
*/

/*
//booking dist id
BookingId bookingId = new BookingId();
string SearchedLocation = "Toscana Italy";
var res10 = bookingId.BookingIDAsync(SearchedLocation);
foreach (var item in res10.Result)
{
    Console.WriteLine(item.Name+""+ item.DestinationType + " " + item.DestId);

}    
*/
/*
//booking
Booking booking = new Booking();
string CheckInDate = "2023-09-27";
string DestType = "region";
string CheckOutDate = "2023-09-30";
string NumOfAdults= "2";
string DestId= "7685";
string NumOfRooms= "3";
string NumOfChildren = "2";
string AgeOfChildren = "10,12";
var res11 = booking.BookingHotelAsync(CheckInDate, DestType, CheckOutDate, NumOfAdults, DestId, NumOfRooms, NumOfChildren, AgeOfChildren);
foreach (var item in res11.Result.Hotels) 
{
    Console.WriteLine(item.Name+" "+item.Url+" "+item.MinTotalPrice+" "+item.Address );
}
*/
/*
DB_DELETE dbDelete = new DB_DELETE();
int idToDelete = 6; // Здесь укажите фактический идентификатор, который нужно удалить
await dbDelete.DeleteAsync(idToDelete);
*/
/*
DB_GET dB_GET = new DB_GET();
var res = dB_GET.GetAsync();



for (int i = 0; i < res.Result.Count; i++)
{
    Console.WriteLine(res.Result[i].name);
}
*/
/*
DB_POST dbPost = new DB_POST();
FavoriteDishPost favoriteDish = new FavoriteDishPost
{
    id = 0,
    chatID = 123,
    name = "Favorite Dish",
    rating = 5
};

HttpResponseMessage response = await dbPost.PostAsync(favoriteDish);
*/
/*
TranslateClient translateClient1 = new TranslateClient();
string text3 = "мое имя густаво";
string fr = "ru";
string to = "en";
var res = translateClient1.TranslateAsync(text3, fr, to).Result;
Console.WriteLine(res.data.translatedText);
*/


