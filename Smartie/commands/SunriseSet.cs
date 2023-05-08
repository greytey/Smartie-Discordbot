using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using CoordinateSharp;
using System.Net.Http;
using DSharpPlus.Entities;
using GoogleMaps.LocationServices;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using AngleSharp.Common;

namespace Smartie.commands
{
    [Group("sun")]
    public class SunriseSet : BaseCommandModule
    {

        [Command("sunrise")]
        public async Task Sunrise(CommandContext ctx, [RemainingText] string location)
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://maps.googleapis.com");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                // Get data response
                location = location.Replace(" ", "+");
                var response = client.GetAsync("/maps/api/geocode/json?address="+location+"&key=AIzaSyASRuXicrlsCBrQFuVxR7igfkn4p6_yBB8").Result;

                float latitude = 0f;
                float longitude = 0f;
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body
                        JObject joResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
                        latitude = (float)joResponse["results"][0]["geometry"]["location"]["lat"];
                        longitude = (float)joResponse["results"][0]["geometry"]["location"]["lng"];
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                                  response.ReasonPhrase);
                }


                Coordinate c = new Coordinate();
                c.GeoDate = DateTime.Now.ToLocalTime();

                CoordinatePart lat;
                CoordinatePart.TryParse(latitude.ToString(), out lat);

                string strlongitude = "";

                if (longitude < 0)
                {
                    strlongitude = "w" + Math.Abs(longitude);
                }

                else
                {
                    strlongitude = "e" + longitude;
                }

                CoordinatePart lng;
                CoordinatePart.TryParse(strlongitude, out lng);
                c.Latitude = lat;
                c.Longitude = lng;

                DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Time of sunrise in " + location.Replace("+", " "),
                    Description = "The time of sunrise is at " + c.CelestialInfo.SunRise.ToString(),
                    Color = DiscordColor.Orange
                };
              
                await ctx.Channel.SendMessageAsync(embed: embededMessage);
            }

            catch (Exception ex)
            {
                DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Error",
                    Description = "Error: " + ex.Message,
                    Color = DiscordColor.Red
                };

                await ctx.Channel.SendMessageAsync(embed: embededMessage);
            }

        }


        [Command("sunset")]
        public async Task Sunset(CommandContext ctx, [RemainingText] string location)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://maps.googleapis.com");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));
                // Get data response
                location = location.Replace(" ", "+");
                var response = client.GetAsync("/maps/api/geocode/json?address=" + location + "&key=AIzaSyASRuXicrlsCBrQFuVxR7igfkn4p6_yBB8").Result;

                float latitude = 0f;
                float longitude = 0f;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body
                    JObject joResponse = JObject.Parse(await response.Content.ReadAsStringAsync());
                    latitude = (float)joResponse["results"][0]["geometry"]["location"]["lat"];
                    longitude = (float)joResponse["results"][0]["geometry"]["location"]["lng"];
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                                  response.ReasonPhrase);
                }


                Coordinate c = new Coordinate();
                c.GeoDate = DateTime.Now.ToLocalTime();

                CoordinatePart lat;
                CoordinatePart.TryParse(latitude.ToString(), out lat);

                string strlongitude = "";

                if (longitude < 0)
                {
                    strlongitude = "w" + Math.Abs(longitude);
                }

                else
                {
                    strlongitude = "e" + longitude;
                }

                CoordinatePart lng;
                CoordinatePart.TryParse(strlongitude, out lng);
                c.Latitude = lat;
                c.Longitude = lng;

                DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Time of sunset in " + location.Replace("+", " "),
                    Description = "The time of sunset is at " + c.CelestialInfo.SunSet.ToString(),
                    Color = DiscordColor.Orange
                };

                await ctx.Channel.SendMessageAsync(embed: embededMessage);
            }

            catch (Exception ex)
            {
                DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
                {
                    Title = "Error",
                    Description = "Error: " + ex.Message,
                    Color = DiscordColor.Red
                };

                await ctx.Channel.SendMessageAsync(embed: embededMessage);
            }

        }

        [Command("help")]
        public async Task help(CommandContext ctx)
        {
            DiscordEmbedBuilder embededMessage = new DiscordEmbedBuilder()
            {
                Title = "Sun help",
                Description = "Sun has the following commands:\n" +
                "- sunrise <location with country>\\ answer with sunrise time and given location\n" +
                "- sunset <location with country>\\ answer with sunset time and given location\n" +
                "\n\n**Remember**: Commands should always start with 'Hey Smartie '",
                Color = DiscordColor.Blue
            };
            await ctx.Channel.SendMessageAsync(embed: embededMessage);
        }
    }
}