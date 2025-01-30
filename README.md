# PosTech.Fase1.Contatos

# Postech.Fase2.Contatos
modelo Url Prometheus
http://localhost:9090/graph?g0.expr=dotnet_total_memory_bytes&g0.tab=0&g0.display_mode=stacked&g0.show_exemplars=0&g0.range_input=15m&g1.expr=irate(process_cpu_seconds_total%5B1m%5D)&g1.tab=0&g1.display_mode=lines&g1.show_exemplars=0&g1.range_input=1h&g2.expr=rate(http_request_duration_seconds_sum%5B3m%5D)%2Frate(http_request_duration_seconds_count%5B3m%5D)&g2.tab=0&g2.display_mode=lines&g2.show_exemplars=0&g2.range_input=1m&g2.end_input=2024-09-24%2021%3A17%3A24&g2.moment_input=2024-09-24%2021%3A17%3A24


#comandos docker

Api
docker build . -f PosTech.Fase1.Contatos.Api/Dockerfile -t  postech-contatos:1.0

Update 
docker build . -f Postech.Fase3.Contatos.Update.Service/Dockerfile -t  postech-update:1.0

Delete
docker build . -f Postech.Fase3.Contatos.Delete.Service/Dockerfile -t  postech-delete:1.0

Add
docker build . -f Postech.Fase3.Contatos.Add.Service/Dockerfile -t  postech-add:1.0


kubernetes dashboard
https://github.com/kubernetes/dashboard/blob/master/docs/user/access-control/creating-sample-user.md

reiniciar kong dashboard
kubectl delete pod kubernetes-dashboard-kong-78fd98d579-sscqt -n kubernetes-dashboard

teste
 dashboard
 kubectl -n kubernetes-dashboard port-forward svc/kubernetes-dashboard-kong-proxy 8443:443
    rabbit    
 kubectl port-forward service/rabbitmq 15672:15672


#prometheus
helm install prometheus prometheus-community/prometheus --namespace monitoramento --create-namespace --set server.service.type=LoadBalancer

helm upgrade --install prometheus prometheus-community/prometheus  --namespace monitoramento --set server.service.type=LoadBalancer --set nodeExporter.hostRootFsMountPropagation=Bidirectional
