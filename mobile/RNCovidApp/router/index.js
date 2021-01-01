import * as React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { FontAwesomeIcon } from '@fortawesome/react-native-fontawesome'
import { faChartArea } from '@fortawesome/free-solid-svg-icons'

import Daily from '../pages/daily'
import Monthly from '../pages/monthly'
import Yearly from '../pages/yearly'

const Tab = createBottomTabNavigator();

const router = () => (
    <NavigationContainer>
        <Tab.Navigator
            initialRouteName="covid"
            tabBarOptions={{
                activeTintColor: '#e91e63',
            }}
        >
            <Tab.Screen name="Günlük" component={Daily} options={{
                tabBarIcon: ({ color, size }) => (
                    <FontAwesomeIcon color={color} size={size} icon={faChartArea} />
                ),
            }} />
            <Tab.Screen name="Aylık" component={Monthly} options={{
                tabBarIcon: ({ color, size }) => (
                    <FontAwesomeIcon color={color} size={size} icon={faChartArea} />
                ),
            }} />
            <Tab.Screen name="Yıllık" component={Yearly} options={{
                tabBarIcon: ({ color, size }) => (
                    <FontAwesomeIcon color={color} size={size} icon={faChartArea} />
                ),
            }} />
        </Tab.Navigator>
    </NavigationContainer>
)

export default router;