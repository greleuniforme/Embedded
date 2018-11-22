package com.example.corentin.app.ApiInterface;

import com.example.corentin.app.ApiInterface.ApiModels.Account.Login.LoginUserModel;
import com.example.corentin.app.ApiInterface.ApiModels.Account.Register.RegisterUserModel;
import com.example.corentin.app.ApiInterface.ApiModels.Account.Token.TokenUserModel;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.Headers;
import retrofit2.http.POST;

public interface ILocalApiEndpointInterface {
    @POST("/api/account/login")
    Call<TokenUserModel> login(@Body LoginUserModel login);

    @POST("/api/account/register")
    Call register(@Body RegisterUserModel login);

    @Headers({"Authorization: Bearer: {token}"})
    @GET("/api/account/islogged")
    Call islogged(@Header("token") String token);

    @Headers({"Authorization: Bearer: {token}"})
    @GET("/api/account/role")
    Call<String> role(@Header("token") String token);

    @Headers({"Authorization: Bearer: {token}"})
    @GET("/api/trainers/listTrainers")
    Call<List<String>> listTrainers(@Header("token") String token);



    @Headers({"Authorization: Bearer: {token}"})
    @GET("/api/trainers/listUsers")
    Call<List<String>> listUsers(@Header("token") String token);
}