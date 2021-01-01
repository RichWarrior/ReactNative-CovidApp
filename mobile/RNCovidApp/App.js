import * as React from 'react';
// import { View, Text } from 'react-native';
// import { NavigationContainer } from '@react-navigation/native';
// import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
// import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
// import { faChartArea } from '@fortawesome/free-solid-svg-icons'
import Router from './router/index'
import * as eva from '@eva-design/eva';
import { ApplicationProvider } from '@ui-kitten/components';

// function HomeScreen() {
//   return (
//     <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
//       <Text>Home Screen</Text>
//     </View>
//   );
// }

// const Tab = createBottomTabNavigator();

// function MyTabs() {
//   return (
//     <NavigationContainer>
//       <Tab.Navigator
//         initialRouteName="covid"
//         tabBarOptions={{
//           activeTintColor: '#e91e63',
//         }}
//       >
//         <Tab.Screen name="Günlük" component={HomeScreen} options={{
//           tabBarIcon: ({ color, size }) => (
//             <FontAwesomeIcon color={color} size={size} icon={faChartArea} />
//           ),
//         }} />
//         <Tab.Screen name="Aylık" component={HomeScreen} options={{
//           tabBarIcon: ({ color, size }) => (
//             <FontAwesomeIcon color={color} size={size} icon={faChartArea} />
//           ),
//         }} />
//         <Tab.Screen name="Yıllık" component={HomeScreen} options={{
//           tabBarIcon: ({ color, size }) => (
//             <FontAwesomeIcon color={color} size={size} icon={faChartArea} />
//           ),
//         }} />
//       </Tab.Navigator>
//     </NavigationContainer>
//   );
// }

export default () => (
  <ApplicationProvider {...eva} theme={eva.light}>
    <Router/>
  </ApplicationProvider>
);