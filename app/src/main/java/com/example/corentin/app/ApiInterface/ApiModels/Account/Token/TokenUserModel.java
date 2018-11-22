package com.example.corentin.app.ApiInterface.ApiModels.Account.Token;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class TokenUserModel {
    @SerializedName("token")
    @Expose
    private String token;
    @SerializedName("refresh_token")
    @Expose
    private String refreshToken;

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public String getRefreshToken() {
        return refreshToken;
    }

    public void setRefreshToken(String refreshToken) {
        this.refreshToken = refreshToken;
    }
}
