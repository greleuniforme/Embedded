package com.example.corentin.app.DatabaseClass;

import com.couchbase.lite.Document;

import java.util.Map;

/**
 * Created by grele on 05/02/2018.
 */

public interface IDocument {
    Map<String, Object> ToMap();
    void UnMap(Map<String, Object> ToUnMap);
    void DocumentToField(Document ToSave);
}
