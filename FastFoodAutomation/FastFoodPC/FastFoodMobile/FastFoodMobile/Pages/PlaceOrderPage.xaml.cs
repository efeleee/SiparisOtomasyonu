﻿using FastFoodMobile.Models;
using FastFoodMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceOrderPage : ContentPage
    {
        private double _totalPrice;
        public PlaceOrderPage(double totalPrice)
        {
            InitializeComponent();
            _totalPrice = totalPrice;
        }

        private async void BtnPlaceOrder_Clicked(object sender, EventArgs e)
        {
           var order = new Order();
            order.fullName = EntName.Text;
            order.phone = EntPhone.Text;
            order.address = EntAddress.Text;
            order.userId = Preferences.Get("userId", 0);
            order.orderTotal = _totalPrice;

           var response = await ApiService.PlaceOrder(order);
            if (response!=null)
            {
                await DisplayAlert("", "Sipariş Numaranız " + response.orderId, "Tamam");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await DisplayAlert("Hata", "Siparişiniz oluşturulurken bir hata oluştu.", "Tamam");
            }
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}