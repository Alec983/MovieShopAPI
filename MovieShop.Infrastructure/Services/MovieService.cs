using MovieShop.Core.ApiModels.Request;
using MovieShop.Core.ApiModels.Response;
using MovieShop.Core.Entities;
using MovieShop.Core.Helpers;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public MovieService(IMovieRepository movieRepository, IFavoriteRepository favoriteRepository)
        {
            _movieRepository = movieRepository;
            _favoriteRepository = favoriteRepository;
        }
        public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
        {
            var temp = new Movie
            {
                BackdropUrl = movieCreateRequest.BackdropUrl,
                Budget = movieCreateRequest.Budget,
                Id = movieCreateRequest.Id,
                ImdbUrl = movieCreateRequest.ImdbUrl,
                PosterUrl = movieCreateRequest.PosterUrl,
                Overview = movieCreateRequest.Overview,
                Price = movieCreateRequest.Price,
                ReleaseDate = movieCreateRequest.ReleaseDate,
                TmdbUrl = movieCreateRequest.TmdbUrl,
                Title = movieCreateRequest.Title,
                Tagline = movieCreateRequest.Tagline,
                RunTime = movieCreateRequest.RunTime,
                Revenue = movieCreateRequest.Revenue,
                CreatedDate = DateTime.Today

            };
            await _movieRepository.AddAsync(temp);
            return new MovieDetailsResponseModel
            {
                BackdropUrl = movieCreateRequest.BackdropUrl,
                Budget = movieCreateRequest.Budget,
                Id = movieCreateRequest.Id,
                ImdbUrl = movieCreateRequest.ImdbUrl,
                PosterUrl = movieCreateRequest.PosterUrl,
                Overview = movieCreateRequest.Overview,
                Price = movieCreateRequest.Price,
                ReleaseDate = movieCreateRequest.ReleaseDate,
                TmdbUrl = movieCreateRequest.TmdbUrl,
                Title = movieCreateRequest.Title,
                Tagline = movieCreateRequest.Tagline,
                RunTime = movieCreateRequest.RunTime,
                Revenue = movieCreateRequest.Revenue
            };
        }

        public Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0)
        {
             
            throw new NotImplementedException();
        }

        public Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId)
        {
            
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies()
        {
            var movies = await _movieRepository.GetHighestGrossingMovies();
            var responseMovies = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                responseMovies.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title,
                }) ;
            }
            return responseMovies;
        }

        public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            var count = await _favoriteRepository.GetFavoriteCount(id);
            var responseMovie = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Rating = movie.Rating,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate.Value,
                RunTime = movie.RunTime,
                Price = movie.Price,
                FavoritesCount = count
            };
            return responseMovie;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId);
            var responseMovies = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                responseMovies.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title,
                });
            }
            return responseMovies;
        }

        public async Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "")
        {
            var task = await _movieRepository.GetPagedData(page, pageSize);
            var responseMovies = new List<MovieResponseModel>();
            foreach (var item in task)
            {

                responseMovies.Add(new MovieResponseModel
                {
                    Id = item.Id,
                    PosterUrl = item.PosterUrl,
                    ReleaseDate = item.ReleaseDate.Value,
                    Title = item.Title

                });

            }
            var set = new PagedResultSet<MovieResponseModel>(responseMovies, pageSize, page, 10);
            return set;
        }

        public async Task<int> GetMoviesCount(string title = "")
        {
            return await _movieRepository.GetCountAsync(m => m.Title == title);
        }

        public async Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
        {
            var reviews =await _movieRepository.GetMovieReviews(id);
            var responseReviews = new List<ReviewMovieResponseModel>();
            foreach (var review in reviews)
            {
                responseReviews.Add(new ReviewMovieResponseModel
                {
                    UserId = review.UserId,
                    MovieId = review.MovieId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating,
                    Name = review.User.FirstName + " " + review.User.LastName,
                });
            }
            return responseReviews;
        }

        public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetTopRatedMovies();
            var responseMovies = new List<MovieResponseModel>();
            foreach (var movie in movies)
            {
                responseMovies.Add(new MovieResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    ReleaseDate = movie.ReleaseDate.Value,
                    Title = movie.Title,
                });
            }
            return responseMovies;
        }

        public Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
