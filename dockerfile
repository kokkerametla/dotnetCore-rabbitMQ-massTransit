
#get rabbitmq base image
FROM rabbitmq:3.11-management

# maintainer
RUN apt-get update && apt-get install -y curl unzip

# install elixir
RUN curl -L https://github.com/noxdafox/rabbitmq-message-deduplication/releases/download/0.6.1/elixir-1.13.4.ez -o /opt/rabbitmq/plugins/elixir-1.13.4.ez && \
    chmod 644 /opt/rabbitmq/plugins/elixir-1.13.4.ez

# install rabbitmq_message_deduplication
RUN curl -L https://github.com/noxdafox/rabbitmq-message-deduplication/releases/download/0.6.1/rabbitmq_message_deduplication-0.6.1.ez -o /opt/rabbitmq/plugins/rabbitmq_message_deduplication-0.6.1.ez && \
    chmod 644 /opt/rabbitmq/plugins/rabbitmq_message_deduplication-0.6.1.ez

#extract elixir
RUN unzip /opt/rabbitmq/plugins/elixir-1.13.4.ez -d /opt/rabbitmq/plugins/elixir-1.13.4

#extract rabbitmq_message_deduplication
RUN unzip /opt/rabbitmq/plugins/rabbitmq_message_deduplication-0.6.1.ez -d /opt/rabbitmq/plugins/rabbitmq_message_deduplication-0.6.1

# enable deduplication plugin
RUN rabbitmq-plugins enable rabbitmq_message_deduplication