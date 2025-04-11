package Models;

public class QueueMessage {
    private final String id;
    private final String message;
    private final Long createdAt;

    public QueueMessage(String id, String message, Long createdAt){
        this.id=id;
        this.message=message;
        this.createdAt = createdAt;
    }

    public String getMessage(){
        return message;
    }

    public String getId(){
        return id;
    }

    public Long getCreatedAt(){
        return createdAt;
    }
}
