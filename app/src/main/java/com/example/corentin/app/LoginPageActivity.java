package com.example.corentin.app;

import android.content.DialogInterface;
import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.design.widget.TextInputEditText;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;

import com.example.corentin.app.ApiInterface.ApiClient;
import com.example.corentin.app.ApiInterface.ApiModels.Account.Login.LoginUserModel;
import com.example.corentin.app.ApiInterface.ApiModels.Account.Token.TokenUserModel;
import com.example.corentin.app.ApiInterface.ILocalApiEndpointInterface;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class LoginPageActivity extends AppCompatActivity {
    ILocalApiEndpointInterface apiInterface;


    public void onClickRegister(View v) {
        Intent i = new Intent(LoginPageActivity.this, RegisterPageActivity.class);
        startActivity(i);
    }

    public void onClickLogged(View v) {
        apiInterface = ApiClient.getClient().create(ILocalApiEndpointInterface.class);

        EditText editEmail = (EditText)findViewById(R.id.editText);
        TextInputEditText editPassword = (TextInputEditText)findViewById(R.id.textInput);

        LoginUserModel user = new LoginUserModel();
        String email = editEmail.getText().toString();
        String password = editPassword.getText().toString();

        user.setEmail(email);
        user.setPassword("Azerty12345");
        Call<TokenUserModel> call = apiInterface.login(user);

        call.enqueue(new Callback<TokenUserModel>() {
            @Override
            public void onResponse(Call<TokenUserModel> call, Response<TokenUserModel> response) {
                TokenUserModel token = response.body();

                Intent i = new Intent(LoginPageActivity.this, LoggedActivity.class);
                startActivity(i);

                AlertDialog alertDialog = new AlertDialog.Builder(LoginPageActivity.this).create();
                alertDialog.setTitle("Alert");
                alertDialog.setMessage(response.message() + " " + response.code() + " " + response.errorBody());
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
                alertDialog.setMessage("triste " + t.getMessage());
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
    }
}
