import * as React from 'react';
import { ScrollView, View, RefreshControl } from 'react-native';
import { Text, Card, Datepicker, Button } from '@ui-kitten/components';
import httpClient from '../common/http.client'
import moment from "moment";

class Daily extends React.Component {
  state = {
    data: [],
    onRefresh: false,
    startDate: new Date(),
    endDate: new Date()
  }

  componentDidMount = () => {
    var req = {
      "query": "query{getDataYear{date dailyTest dailyCase dailySick dailyDeath dailyHealing totalTest totalSick totalDeath}}",
      "variables": {
      }
    }
    this.setState({ onRefresh: true })
    this.setState({ data: [] })
    httpClient.Post("/api/Covid", req).then((res) => {
      this.setState({ data: res.data.getDataYear })
    }).catch(() => {
      alert('Sunucu Hatası')
    }).finally(() => {
      this.setState({ onRefresh: false })
    })
  }

  componentWillUnmount = () => {
    this.setState = (state, callback) => {
      return;
    };
  }

  changeStartDate = (date) => {
    this.setState({ startDate: date })
  }

  changeEndDate = (date) => {
    this.setState({ endDate: date })
  }

  onButtonClick = () => {
    var req = {
      "query": "query($startDate : Date!,$endDate : Date!){getDataYearBetweenDates(startDate : $startDate,endDate : $endDate){date dailyTest dailyCase dailySick dailyDeath dailyHealing totalTest totalSick totalDeath}}",
      "variables": {
        "startDate": moment(this.state.startDate).format('YYYY-MM-DD'),
        "endDate": moment(this.state.endDate).format('YYYY-MM-DD')
      }
    }
    this.setState({ onRefresh: true })
    this.setState({ data: [] })
    httpClient.Post("/api/Covid", req).then((res) => {
      this.setState({ data: res.data.getDataYearBetweenDates })
    }).catch(() => {
      alert('Sunucu Hatası')
    }).finally(() => {
      this.setState({ onRefresh: false })
    })
  }

  render() {
    return (
      <ScrollView refreshControl={
        <RefreshControl refreshing={this.state.onRefresh} onRefresh={this.componentDidMount} />
      } style={{ flex: 1 }}>
        <View>
          <Card header={() => (
            <View style={{ alignItems: 'center' }}>
              <Text style={{ padding: 5 }}>Tarih Aralığı Filtrele</Text>
            </View>
          )
          }>
            <Datepicker
              label="Başlangıç Tarihi"
              date={this.state.startDate}
              onSelect={(date) => this.changeStartDate(date)}
            />
            <Datepicker
              label="Bitiş Tarihi"
              date={this.state.endDate}
              onSelect={(date) => this.changeEndDate(date)}
            />

            <Button onPress={this.onButtonClick}>Filtrele</Button>
          </Card>
        </View>
        {this.state.data.map((item, index) => {
          return (
            <Card style={{ margin: 5 }} key={index} header={() => (
              <View style={{ alignItems: 'center', padding: 1 }}>
                <Text>Tarih = {moment(item.date).format('DD/MM/YYYY')}</Text>
              </View>
            )} status='success'>
              <View style={{ flex: 1, alignItems: 'center' }}>
                <Text>Günlük Test {item.dailyTest}</Text>
                <Text>Günlük Vaka {item.dailyCase}</Text>
                <Text>Günlük Hasta {item.dailySick}</Text>
                <Text>Günlük Ölüm {item.dailyDeath}</Text>
                <Text>Günlük İyileşme {item.dailyHealing}</Text>
                <Text>Toplam Test {item.totalTest}</Text>
                <Text>Toplam Hasta {item.totalSick}</Text>
                <Text>Toplam Ölüm {item.totalDeath}</Text>
              </View>
            </Card>
          )
        })}
      </ScrollView>
    )
  }
}

export default Daily;