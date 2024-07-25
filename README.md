
# Case Documentation

Proje microservice üzerine API gateway aracılığı ile inşa edildi.
İlk olarak Şirketlerin üye olabildiği servisimiz var bu taraf Onion Architectureda Postgresql databaseini kullanarak geliştirildi ve diğer servisimiz ise ilan yayınlayabildiği kısım. Burası Elasticsearch kullanarak tam performanslı arama yapma olanağı sunması hedeflendi. 2 servis arasındaki iletişim RabbitMQ ile sağlandı ve Eventual Consistency | Saga Pattern - Orchestration implemente edildi.

Projenin enviroment olarak docker-compose.yml dosyasında ayağa kalkıyor fakat web servisler yani apilerı localde çalıştırmak gerekecektir.

Kullanılan teknolojiler;
.Net Core 8 WEB API
RabbitMQ
Elasticsearch Kibana
Postgresql
Onion Architecture
N-layer Architecture
Fluent Validation
Docker




