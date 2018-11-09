package com.example.corentin.app.Fragments;

import android.os.Bundle;
import android.support.v4.app.ListFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.SimpleAdapter;
import android.widget.Toast;

import com.example.corentin.app.R;

import java.util.ArrayList;
import java.util.HashMap;

public class TrainersFragment extends ListFragment {
    String[] players = {"Abdi Stevens", "Eryk Corbett", "Grady Ahmed", "David Zhang", "Kristian Hooper", "Van Persie", "Oscar"};
    String[] experience = {
            "Perfection is not attainable, but if we chase perfection we can catch excellence",
            "Health & Fitness Lifestyle Transformation.Gym doesn't change live, People do.",
            "If we chase perfection we can catch excellence",
            "Gym doesn't change live, People do.",
            "An accomplished fitness trainer with seven years of experience n hand",
            "No pain, No gain",
            "Gym doesn't change live, People do."
    };
    int[] images = {R.drawable.ic_trainers, R.drawable.ic_trainers, R.drawable.ic_trainers, R.drawable.ic_trainers, R.drawable.ic_trainers, R.drawable.ic_trainers, R.drawable.ic_trainers};

    ArrayList<HashMap<String, String>> data = new ArrayList<HashMap<String, String>>();
    SimpleAdapter adapter;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // TODO Auto-generated method stub

        //MAP
        HashMap<String, String> map = new HashMap<String, String>();

        //FILL
        for (int i = 0; i < players.length; i++) {
            map = new HashMap<String, String>();
            map.put("Player", players[i]);
            map.put("Info",experience[i]);
            map.put("Image", Integer.toString(images[i]));

            data.add(map);
        }

        //KEYS IN MAP
        String[] from = {"Player","Info", "Image"};

        //IDS OF VIEWS
        int[] to = {R.id.nameTxt, R.id.infoTxt, R.id.imageView1};

        //ADAPTER
        adapter = new SimpleAdapter(getActivity().getBaseContext(), data, R.layout.trainerlist_item, from, to);
        setListAdapter(adapter);

        return super.onCreateView(inflater, container, savedInstanceState);
    }

    @Override
    public void onStart() {
        // TODO Auto-generated method stub
        super.onStart();

        getListView().setOnItemClickListener(new AdapterView.OnItemClickListener() {

            @Override
            public void onItemClick(AdapterView<?> av, View v, int pos,
                                    long id) {
                // TODO Auto-generated method stub

                Toast.makeText(getActivity(), data.get(pos).get("Player"), Toast.LENGTH_SHORT).show();

            }
        });
    }

}