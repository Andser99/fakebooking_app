using MobileBooking.Models;
using MobileBooking.Services;
using MobileBooking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileBooking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewPage : ContentPage
    {
        private ReservationItem CurrentReservation;

        public ReviewViewModel reviewViewModel;
        public ReviewPage(ReservationItem reservationItem)
        {
            BindingContext = reviewViewModel = new ReviewViewModel();
            InitializeComponent();
            reviewViewModel.hotel_name = reservationItem.hotel_name;
            reviewViewModel.reserved_from = reservationItem.reserved_from;
            reviewViewModel.reserved_to = reservationItem.reserved_to;
            if (reservationItem.review_id != null)
            {
                CheckReview(Convert.ToInt32(reservationItem.review_id));
            }
            CurrentReservation = reservationItem;

            if (reservationItem.review_id == null)
            {
                submitReviewButton.IsVisible = true;
            }
            else
            {
                updateReviewButton.IsVisible = true;
                deleteReviewButton.IsVisible = true;
            }

        }

        private async void CheckReview(int reviewId)
        {
            var res = await RestService.GetReview(reviewId);
            if (res.Message == "ok")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ratingBar.SelectedStarValue = Convert.ToInt32(res.Review.stars);
                    editor.Text = res.Review.text;
                });
            }
        }

        private async void submitReviewButton_Clicked(object sender, EventArgs e)
        {
            ReviewItem review = new ReviewItem();
            review.hotel_id = CurrentReservation.id;
            review.stars = ratingBar.SelectedStarValue.ToString();
            review.text = editor.Text;
            review.user_id = User.CurrentUser.UserId.ToString();
            review.username = User.CurrentUser.Username;
            var res = await RestService.PostReview(review);
            await DisplayAlert("Result", res, "OK");
            await Navigation.PopAsync();
        }

        private async void deleteReviewButton_Clicked(object sender, EventArgs e)
        {
            var res = await RestService.DeleteReview(Convert.ToInt32(CurrentReservation.review_id));
            await DisplayAlert("Result", res, "OK");
            await Navigation.PopAsync();
            
        }

        private async void updateReviewButton_Clicked(object sender, EventArgs e)
        {
            ReviewItem review = new ReviewItem();
            review.id = CurrentReservation.review_id;
            review.hotel_id = CurrentReservation.id;
            review.stars = ratingBar.SelectedStarValue.ToString();
            review.text = editor.Text;
            review.user_id = User.CurrentUser.UserId.ToString();
            review.username = User.CurrentUser.Username;
            var res = await RestService.PutReview(review);
            await DisplayAlert("Result", res, "OK");
            await Navigation.PopAsync();
        }
    }
}