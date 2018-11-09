package com.example.corentin.app.DatabaseClass;

import com.couchbase.lite.Document;
import com.example.corentin.app.DatabaseClass.IDocument;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

/**
 * Created by grele on 11/02/2018.
 */

public class Token  implements IDocument, Serializable {
    @SerializedName("Id")
    public String Id;
    @SerializedName("Token")
    public String Token;

    public Token() {
        this.Id = UUID.randomUUID().toString();
    }

    public Map<String, Object> ToMap() {
        Map<String, Object> properties = new HashMap<String, Object>();
        properties.put("Id", this.Id);
        properties.put("Token", this.Token);
        return (properties);
    }

    public void UnMap(Map<String, Object> ToUnMap) {
        this.Id = (String)ToUnMap.get("Id");
        this.Token = (String)ToUnMap.get("Token");
    }

    public void DocumentToField(Document ToSave) {
        this.Id = (String)ToSave.getProperty("Id");
        this.Token = (String)ToSave.getProperty("Token");
    }
}
