package com.example.corentin.app.DatabaseClass;

import com.couchbase.lite.Document;
import com.example.corentin.app.DatabaseClass.IDocument;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;


/**
 * Created by grele on 05/02/2018.
 */

public class UploadImage implements IDocument, Serializable {
    @SerializedName("Id")
    public String Id;
    @SerializedName("Title")
    public String Title;
    @SerializedName("Url")
    public String Url;

    public UploadImage() {
        this.Id = UUID.randomUUID().toString();
    }

    public Map<String, Object> ToMap() {
        Map<String, Object> properties = new HashMap<String, Object>();
        properties.put("Id", this.Id);
        properties.put("Title", this.Title);
        properties.put("Url", this.Url);
        return (properties);
    }

    public void UnMap(Map<String, Object> ToUnMap) {
        this.Id = (String)ToUnMap.get("Id");
        this.Title = (String)ToUnMap.get("Title");
        this.Url = (String)ToUnMap.get("Url");
    }

    public void DocumentToField(Document ToSave) {
        this.Id = (String)ToSave.getProperty("Id");
        this.Title = (String)ToSave.getProperty("Title");
        this.Url = (String)ToSave.getProperty("Url");
    }
}