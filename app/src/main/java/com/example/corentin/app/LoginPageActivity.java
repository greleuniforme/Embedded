package com.example.corentin.app;

import android.content.DialogInterface;
import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;

import com.example.corentin.app.ApiInterface.ApiModels.Account.Login.LoginUserModel;
import com.example.corentin.app.ApiInterface.ApiModels.Account.Token.TokenUserModel;
import com.example.corentin.app.ApiInterface.ILocalApiEndpointInterface;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class LoginPageActivity extends AppCompatActivity {


    public void onClickRegister(View v) {
        Intent i = new Intent(LoginPageActivity.this, RegisterPageActivity.class);
        startActivity(i);
    }

    public void onClickLogged(View v) {
        Intent i = new Intent(LoginPageActivity.this, LoggedActivity.class);
        startActivity(i);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login_page);

        BottomNavigationView bottomNavigationView = (BottomNavigationView)
                findViewById(R.id.bottom_navigation);
        bottomNavigationView.setSelectedItemId(R.id.action_account);
        bottomNavigationView.setOnNavigationItemSelectedListener(
                new BottomNavigationView.OnNavigationItemSelectedListener() {
                    @Override
                    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                        switch (item.getItemId()) {
                            case R.id.action_home:
                                Intent i = new Intent(LoginPageActivity.this, MainActivity.class);
                                i.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
                                startActivity(i);
                        }
                        return true;
                    }
                });

       Retrofit retrofit = new Retrofit.Builder()
              .baseUrl(getString(R.string.api_url))
              .addConverterFactory(GsonConverterFactory.create())
              .build();

        ILocalApiEndpointInterface apiService =
                retrofit.create(ILocalApiEndpointInterface.class);

        LoginUserModel user = new LoginUserModel();
        user.setEmail("admin");
        user.setPassword("Azerty12345");
        Call<TokenUserModel> call = apiService.login(user);
        call.enqueue(new Callback<TokenUserModel>() {
            @Override
            public void onResponse(Call<TokenUserModel> call, Response<TokenUserModel> response) {
                TokenUserModel token = response.body();

                AlertDialog alertDialog = new AlertDialog.Builder(LoginPageActivity.this).create();
                alertDialog.setTitle("Alert");
                alertDialog.setMessage(token.getToken());
                alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                dialog.dismiss();
                            }
                        });
                alertDialog.show();
            }

            @Override
            public void onFailure(Call<TokenUserModel> call, Throwable t) {
                AlertDialog alertDialog = new AlertDialog.Builder(LoginPageActivity.this).create();
                alertDialog.setTitle("Alert");
                alertDialog.setMessage("triste");
                alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {
                                dialog.dismiss();
                            }
                        });
                alertDialog.show();
            }
        });
    }
}
