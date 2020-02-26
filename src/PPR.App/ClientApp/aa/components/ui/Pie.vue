<script>
    import {Pie} from 'vue-chartjs'

    export default {

        extends: Pie,

        props:{

            chartData :{
        
                type: Array,
                
                required:false
            
            },
            
            chartLabel:{
            
                type:Array,
                
                required:false
            
            }
        
        },
        mounted () {

            this.renderChart({
        
                    labels: this.chartLabel,
                    
                    datasets: [
                    
                        {
                            label: 'Email Statistics',
                        
                            data: this.chartData,
                            
                            backgroundColor: ["#bb8b30","#1a7bb9","#ed5565","#262626","#23c6c8","#f8ac59"]
                        
                        }
                    ]
                    
            }, {responsive: true, maintainAspectRatio: false})

        }
    }

</script>
