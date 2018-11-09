package com.example.corentin.app;

import java.util.Map;
import com.couchbase.lite.Context;
import com.couchbase.lite.Database;
import com.couchbase.lite.Document;
import com.couchbase.lite.Manager;
import com.couchbase.lite.QueryOptions;
import com.couchbase.lite.UnsavedRevision;
import com.example.corentin.app.DatabaseClass.IDocument;


/**
 * Created by grele on 05/02/2018.
 */
public class DatabaseConf {
    Manager _manage = null;
    Database _myDb = null;
    public DatabaseConf(Context MyContext, String DbName){
        try {
            _manage = new Manager(MyContext, Manager.DEFAULT_OPTIONS);
            _myDb = _manage.getDatabase(DbName);
        }
        catch (Exception e){
        }
    }
    public void CreateOrUpDate(IDocument MyDoc){
        Document CurrentDoc = _myDb.createDocument();
        Map<String, Object> MyProperties = MyDoc.ToMap();

        try {
            CurrentDoc.putProperties(MyProperties);
        }
        catch (Exception e){
        }
    }

    public void UpDate(IDocument MyDoc, String Id){
        final Map<String, Object> MyProperties = MyDoc.ToMap();

        try {
            Document document = _myDb.getDocument(Id);
            document.update(new Document.DocumentUpdater() {
                @Override
                public boolean update(UnsavedRevision newRevision) {
                    newRevision.setUserProperties(MyProperties);
                    return true;
                }
            });
        }
        catch (Exception e){
        }
    }


    public void DeleteById(Document id) {
        try {
            id.delete();
        }
        catch (Exception e){
        }
    }

    public Document getDocumentById(String documentId){
        Document document =  _myDb.getDocument(documentId);
        return (document);
    }

    public Map<String, Object> getAllDocument(){
        QueryOptions myq = new QueryOptions();
        myq.setAllDocsMode(myq.getAllDocsMode());
        try {
            Map<String, Object> AllDoc  = _myDb.getAllDocs(myq);
            return (AllDoc);
        }
        catch(Exception e) {
        }
        return (null);
    }
}
