package com.example.corentin.app.ApiInterface;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ApiClient {

    private static Retrofit retrofit = null;

    public static Retrofit getClient() {
//        final OkHttpClient okHttpClient = new OkHttpClient.Builder()
//                .connectTimeout(40, TimeUnit.SECONDS)
//                .writeTimeout(40, TimeUnit.SECONDS)
//                .readTimeout(60, TimeUnit.SECONDS)
//                .build();
//                .client(okHttpClient)

        retrofit = new Retrofit.Builder()
                .baseUrl("http://0fc99a5b.ngrok.io/")
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        return retrofit;
    }
}