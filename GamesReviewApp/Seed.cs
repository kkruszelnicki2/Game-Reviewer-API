using GamesReviewApp.Data;
using GamesReviewApp.Models;

namespace GamesReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.GameProducers.Any())
            {
                var gameProducers = new List<GameProducent>()
                {
                    new GameProducent()
                    {
                        Game = new Game()
                        {
                            Name = "Minecraft",
                            ReleaseDate = new DateTime(2011,11,18),
                            GameTags = new List<GameTag>()
                            {
                                new GameTag { Tag = new Tag() { Name = "Survival"} },
                                new GameTag { Tag = new Tag() { Name = "Sandbox"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Text = "Must play for every gamer!", Rate = 5,
                                Reviewer = new Reviewer(){ Name = "Teddy"} },
                                new Review { Text = "Very nostalgic", Rate = 5,
                                Reviewer = new Reviewer(){ Name = "Taylor"} },
                                new Review { Text = "Boring", Rate = 1,
                                Reviewer = new Reviewer(){ Name = "Jessica"} },
                            }
                        },
                        Producent = new Producent()
                        {
                            Name = "Mohjang Studio",
                            Country = new Country()
                            {
                                Name = "Sweden"
                            }
                        },
                    },
                    new GameProducent()
                    {
                        Game = new Game()
                        {
                            Name = "League of Legends",
                            ReleaseDate = new DateTime(2009,10,27),
                            GameTags = new List<GameTag>()
                            {
                                new GameTag { Tag = new Tag() { Name = "MOBA"} },
                                new GameTag { Tag = new Tag() { Name = "Online"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Text = "I hate this game!", Rate = 5,
                                Reviewer = new Reviewer(){ Name = "Noah"} },
                                new Review { Text = "My main got nerfed stupid game!", Rate = 2,
                                Reviewer = new Reviewer(){ Name = "Yassuo"} },
                                new Review { Text = "Season 8 was better", Rate = 3,
                                Reviewer = new Reviewer(){ Name = "Faker"} },
                            }
                        },
                        Producent = new Producent()
                        {
                            Name = "Riot Games",
                            Country = new Country()
                            {
                                Name = "USA"
                            }
                        }
                    },
                    new GameProducent()
                    {
                        Game = new Game()
                        {
                            Name = "Counter Strike: Global Offensive",
                            ReleaseDate = new DateTime(2012,8,21),
                            GameTags = new List<GameTag>()
                            {
                                new GameTag { Tag = new Tag() { Name = "Shooter"}},
                                new GameTag { Tag = new Tag() { Name = "Online"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Text = "Cheaters everywhere!", Rate = 1,
                                Reviewer = new Reviewer(){ Name = "Dante"} },
                                new Review { Text = "Just got a knive from chest, best day of my life!", Rate = 5,
                                Reviewer = new Reviewer(){ Name = "Isac"} },
                                new Review { Text = "League of Legends is better...", Rate = 2,
                                Reviewer = new Reviewer(){ Name = "Covetous"} },
                            }
                        },
                        Producent = new Producent()
                        {
                            Name = "Valve",
                            Country = new Country()
                            {
                                Name = "USA"
                            }
                        }
                    },
                    new GameProducent()
                    {
                        Game = new Game()
                        {
                            Name = "Final Fantasy XIII",
                            ReleaseDate = new DateTime(2009,12,17),
                            GameTags = new List<GameTag>()
                            {
                                new GameTag { Tag = new Tag() { Name = "JRPG"} }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Text = "Lightning is waifu!", Rate = 5,
                                Reviewer = new Reviewer(){ Name = "NerdGuy"} },
                                new Review { Text = "Not as good as Final Fantasy VII, but still decent", Rate = 4,
                                Reviewer = new Reviewer(){ Name = "FinalFantasyLover"} },
                                new Review { Text = "League of Legends is better...", Rate = 2,
                                Reviewer = new Reviewer(){ Name = "Covetous"} },
                            }
                        },
                        Producent = new Producent()
                        {
                            Name = "Square Enix",
                            Country = new Country()
                            {
                                Name = "Japan"
                            }
                        }
                    },
                };


                dataContext.GameProducers.AddRange(gameProducers);
                dataContext.SaveChanges();
            }
        }
    }
}